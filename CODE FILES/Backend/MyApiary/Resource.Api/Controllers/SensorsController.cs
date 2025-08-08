using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resource.Api.Confirmations;
using Resource.Api.Database;
using Resource.Api.Entities;
using Resource.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public SensorsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Sensors/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<ESensor> Get_sensors()
        {
            return _context.Sensors;
        }

        // GET: api/Sensors/GetSensorsByBeehive/beehive_id
        [HttpGet]
        [Route("GetSensorsByBeehive/{beehive_id}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetSensorsByBeehive([FromRoute] int beehive_id)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Apiaries.Find(_context.Beehives.Find(beehive_id).ApiaryId).UserId)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EBeehive> beehives = GetUserElements.GetUserBeehives(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)), _context);
                if (beehives == null || beehives.Count == 0)
                {
                    return NotFound();
                }
            }

            List<ESensor> sensors = new List<ESensor>();
            sensors.AddRange(_context.Sensors.Where(x => x.BeehiveId == beehive_id).ToList());

            List<SensorsGetResponce> sensorsGetResponces = new List<SensorsGetResponce>();

            foreach(var sensor in sensors)
            {
                sensorsGetResponces.Add(new SensorsGetResponce()
                {
                    Id = sensor.Id,
                    MinValue = sensor.Min_value,
                    MaxValue = sensor.Max_value,
                    isWorking = sensor.Is_working,
                    Type = _context.SensorTypes.Find(sensor.SensorTypeId).Name,
                    SerialNumber = sensor.Serial_number,
                    BeehiveId = sensor.BeehiveId,
                    BaseStationName = _context.BaseStations.Find(sensor.BaseStationId).Name,
                    BaseStationSerialNumber = _context.BaseStations.Find(sensor.BaseStationId).Serial_number
                });
            }

            if (sensorsGetResponces == null || sensorsGetResponces.Count == 0)
            {
                return NotFound();
            }

            return Ok(sensorsGetResponces);
        }

        // GET: api/Sensors/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get_sensor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                EUser user = _context.Users.Find(Convert.ToInt32(User.FindFirstValue("Sub")));

                List<ESensor> Sensors = GetUserElements.GetUserSensors(user.Id, _context);

                if (Sensors == null || Sensors.Count == 0 || Sensors.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var sensor = await _context.Sensors.FindAsync(id);

            if (sensor == null)
            {
                return NotFound();
            }

            return Ok(sensor);
        }


        // PUT: api/Sensors/editbyuser
        [HttpPut]
        [Route("editbyuser")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> SensorPutByUser([FromBody] List<SensorsUpdateRequest> sensorsUpdates)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Apiaries.Find(_context.Beehives.Find(sensorsUpdates[0].beehiveId).ApiaryId).UserId)
            {
                return BadRequest(ModelState);
            }

            foreach (var sensor in sensorsUpdates)
            {

                ESensor eSensor = _context.Sensors.Find(sensor.sensor_id);

                if (eSensor != null)
                {
                    double min = Math.Round(Convert.ToDouble(sensor.minValue), 1);
                    double max = Math.Round(Convert.ToDouble(sensor.maxValue), 1);

                    _context.Entry(eSensor).Property(x => x.Min_value).CurrentValue = (float) min;
                    _context.Entry(eSensor).Property(x => x.Max_value).CurrentValue = (float) max;
                    _context.Entry(eSensor).Property(x => x.Is_working).CurrentValue = sensor.isWorking;

                    _context.Entry(eSensor).Property(x => x.Min_value).IsModified = true;
                    _context.Entry(eSensor).Property(x => x.Max_value).IsModified = true;
                    _context.Entry(eSensor).Property(x => x.Is_working).IsModified = true;

                    //_context.Entry(eSensor).State = EntityState.Modified;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

            return Ok(new { updated_sensors = true });
        }

        // PUT: api/Sensors/editValue
        [HttpPut]
        [Route("editValue")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put_sensor_value([FromBody] List<ESensor> sensors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach(var sensor in sensors)
            {

                ESensor sensUpdate = _context.Sensors.Find(sensor.Id);
                if (sensUpdate == null) continue;

                ////////////////// Alarm logic ////////////////////

                if (sensor.Value < sensUpdate.Min_value && sensor.Is_working)
                {
                    _context.Entry(_context.Beehives.Find(sensor.BeehiveId)).Property(x => x.Alarm).CurrentValue = true;
                    _context.Entry(_context.Beehives.Find(sensor.BeehiveId)).Property(x => x.Alarm).IsModified = true;
                    //_context.Beehives.Find(sensor.BeehiveId).Alarm = true;

                    EApiary apiary = _context.Apiaries.Find(_context.Beehives.Find(sensor.BeehiveId).ApiaryId);
                    EUser user = _context.Users.Find(apiary.UserId);

                    Mail.sendEmail(user.Mail, $"Датчик типа '{_context.SensorTypes.Find(sensor.SensorTypeId).Name}' превысил минимальное значение({sensUpdate.Min_value}), " +
                        $"и сейчас находится в значении {sensor.Value}. Рекомендуем срочно проверить семью пчёл.");

                    Phone.SendSms(user.Phone, $"Датчик типа '{_context.SensorTypes.Find(sensor.SensorTypeId).Name}' превысил минимальное значение({sensUpdate.Min_value}), " +
                        $"и сейчас находится в значении {sensor.Value}. Рекомендуем срочно проверить семью пчёл.");

                    ENotification notification = new ENotification()
                    {
                        Name = $"{_context.Beehives.Find(sensor.BeehiveId).Name}",
                        Description = $"Датчик типа '{_context.SensorTypes.Find(sensor.SensorTypeId).Name}' превысил минимальное значение({sensUpdate.Min_value}), " +
                        $"и сейчас находится в значении {sensor.Value}. Рекомендуем срочно проверить семью пчёл.",
                        Created_date = DateTime.Now,
                        Read_date = DateTime.Now,
                        Readed = false,
                        ApiaryId = _context.Apiaries.Find(_context.Beehives.Find(sensor.BeehiveId).ApiaryId).Id
                    };

                    _context.Notifications.Add(notification);
                }
                else if (sensor.Value > sensUpdate.Max_value && sensor.Is_working)
                {
                    _context.Entry(_context.Beehives.Find(sensor.BeehiveId)).Property(x => x.Alarm).CurrentValue = true;
                    _context.Entry(_context.Beehives.Find(sensor.BeehiveId)).Property(x => x.Alarm).IsModified = true;
                    //_context.Beehives.Find(sensor.BeehiveId).Alarm = true;

                    EApiary apiary = _context.Apiaries.Find(_context.Beehives.Find(sensor.BeehiveId).ApiaryId);
                    EUser user = _context.Users.Find(apiary.UserId);

                    Mail.sendEmail(user.Mail, $"Датчик типа '{_context.SensorTypes.Find(sensor.SensorTypeId).Name}' превысил максимальное значение({sensUpdate.Min_value}), " +
                        $"и сейчас находится в значении {sensor.Value}. Рекомендуем срочно проверить семью пчёл.");

                    Phone.SendSms(user.Phone, $"Датчик типа '{_context.SensorTypes.Find(sensor.SensorTypeId).Name}' превысил максимальное значение({sensUpdate.Min_value}), " +
                        $"и сейчас находится в значении {sensor.Value}. Рекомендуем срочно проверить семью пчёл.");

                    ENotification notification = new ENotification()
                    {
                        Name = $"{_context.Beehives.Find(sensor.BeehiveId).Name}",
                        Description = $"Датчик типа '{_context.SensorTypes.Find(sensor.SensorTypeId).Name}' превысил минимальное значение({sensUpdate.Min_value}), " +
                        $"и сейчас находится в значении {sensor.Value}. Рекомендуем срочно проверить семью пчёл.",
                        Created_date = DateTime.Now,
                        Read_date = DateTime.Now,
                        Readed = false,
                        ApiaryId = _context.Apiaries.Find(_context.Beehives.Find(sensor.BeehiveId).ApiaryId).Id
                    };

                    _context.Notifications.Add(notification);
                }
                ///////////////////////////////////////////////////

                try
                {
                    if (sensor.Is_working)
                    {
                        _context.Entry(sensUpdate).Property(x => x.Value).CurrentValue = (float) Math.Round(sensor.Value, 1);
                        _context.Entry(sensUpdate).Property(x => x.Value).IsModified = true;
                    }
                    else
                    {
                        _context.Entry(sensUpdate).Property(x => x.Value).CurrentValue = 0;
                        _context.Entry(sensUpdate).Property(x => x.Value).IsModified = true;
                    }
                }
                catch(Exception e)
                {
                    return NotFound(e.Message);
                }

                //_context.Entry(sensor).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }

        // POST: api/Sensors/addbyAdmin
        [HttpPost]
        [Route("addbyAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post_sensor([FromBody] ESensor sensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*if (GetUserElements.AddNewItem(Convert.ToInt32(User.FindFirstValue("Sub")), "Sensor", _context) == false)
            {
                return BadRequest("Достигнуто максимально кол-во Sensors для вашего тарифа.");
            }*/


            if (_context.Sensors.Where(x => x.BeehiveId == sensor.BeehiveId && x.SensorTypeId == sensor.SensorTypeId).ToList().Count() != 0)
                return StatusCode(410);

            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Sensors/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete_sensor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                EUser user = _context.Users.Find(Convert.ToInt32(User.FindFirstValue("Sub")));

                List<ESensor> Sensors = GetUserElements.GetUserSensors(user.Id, _context);

                if (Sensors == null || Sensors.Count == 0 || Sensors.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();

            return Ok(sensor);
        }

        private bool SensorExists(int id)
        {
            return _context.Sensors.Any(e => e.Id == id);
        }
    }
}
