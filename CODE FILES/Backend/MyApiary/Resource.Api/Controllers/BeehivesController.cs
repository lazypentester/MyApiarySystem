using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class BeehivesController : ControllerBase
    {
        private readonly ProjectContext _context;

        public BeehivesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Beehives/get_sensors/{id}
        [HttpGet]
        [Route("get_sensors/{id}")]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<IEnumerable<ESensor>> GetSensors([FromRoute] int id)
        {
            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EBeehive> beehives = GetUserElements.GetUserBeehives(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (beehives == null || beehives.Count == 0 || beehives.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }
            return _context.Sensors.Where(x => x.BeehiveId == id).ToList();
        }

        // GET: api/Beehives/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<EBeehive> GetBeehives()
        {
            return _context.Beehives;
        }

        // GET: api/Beehives/getallsensors/{beehive_id}
        [HttpGet]
        [Route("getallsensors/{beehive_id}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult getAllSensorsByBeehive([FromRoute] int beehive_id)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Apiaries.Find(_context.Beehives.Find(beehive_id).ApiaryId).UserId)
            {
                return BadRequest(ModelState);
            }

            int sensors_count = _context.Sensors.Where(x => x.BeehiveId == beehive_id).Count();

            return Ok(new { sensors_count = sensors_count });
        }

        // GET: api/Beehives/getallbyapiaryid/apiaryid
        [HttpGet]
        [Route("getallbyapiaryid/{apiaryid}")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult GetBeehivesByApiaryId([FromRoute] int apiaryid)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Apiaries.Find(apiaryid).UserId)
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

            var beehives_list = GetApiaryBeehives(apiaryid);

            if (beehives_list == null)
            {
                return NotFound();
            }

            float SensorTemperature = 0;
            float SensorHumidity = 0;
            float SensorNoise = 0;

            List<Beehive> responce = new List<Beehive>();

            foreach (var item in beehives_list)
            {
                SensorTemperature = 0;
                SensorHumidity = 0;
                SensorNoise = 0;

                try
                {
                    foreach(var sensor in GetBeehiveSensors(item.Id))
                    {
                        switch(_context.SensorTypes.Find(sensor.SensorTypeId).Name.ToLower())
                        {
                            case "sensor_temperature":
                                SensorTemperature = sensor.Value;
                                break;
                            case "sensor_noise":
                                SensorNoise = sensor.Value;
                                break;
                            case "sensor_humidity":
                                SensorHumidity = sensor.Value;
                                break;
                            default:
                                return BadRequest(new { error = "Ошибка при переборе значений и совпадений датчиков(сенсоров) для каждого улея." });
                        }
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

                responce.Add(new Beehive()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Alarm = item.Alarm,
                    SensorTemperature = SensorTemperature,
                    SensorHumidity = SensorHumidity,
                    SensorNoise = SensorNoise
                });
            }

            return Ok(responce);
        }

        private List<ESensor> GetBeehiveSensors(int beehive_id)
        {
            List<ESensor> sensors = new List<ESensor>();

            sensors.AddRange(_context.Sensors.Where(x => x.BeehiveId == beehive_id).ToList());

            return sensors;
        }

        private List<EBeehive> GetApiaryBeehives(int apiary_id)
        {
            List<EBeehive> beehives = new List<EBeehive>();

            beehives.AddRange(_context.Beehives.Where(x => x.ApiaryId == apiary_id).ToList());

            return beehives;
        }

        // GET: api/Beehives/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetBeehive([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EBeehive> beehives = GetUserElements.GetUserBeehives(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (beehives == null || beehives.Count == 0 || beehives.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var beehive = await _context.Beehives.FindAsync(id);

            if (beehive == null)
            {
                return NotFound();
            }

            return Ok(beehive);
        }

        // PUT: api/Beehives/edit/5
        [HttpPut]
        [Route("edit/{beehive_id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PutBeehive([FromRoute] int beehive_id, [FromBody] BeehiveUpdate beehive_update)
        {
            if (!ModelState.IsValid || beehive_id != beehive_update.Id || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Apiaries.Find(beehive_update.ApiariId).UserId)
            {
                return BadRequest(ModelState);
            }

            EBeehive eBeehive = _context.Beehives.Find(beehive_id);

            if (eBeehive == null)
            {
                return NotFound();
            }

            _context.Entry(eBeehive).Property(x => x.Name).CurrentValue = beehive_update.Name;
            _context.Entry(eBeehive).Property(x => x.Alarm).CurrentValue = beehive_update.Alarm;

            _context.Entry(eBeehive).Property(x => x.Name).IsModified = true;
            _context.Entry(eBeehive).Property(x => x.Alarm).IsModified = true;

            _context.Entry(eBeehive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

            return Ok(new { updated_beehive = true });
        }

        // POST: api/Beehives/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PostBeehive([FromBody] AddNewBeehive beehive_request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Apiaries.Find(beehive_request.ApiaryId).UserId)
                {
                    return BadRequest("Stop hacking pls...");
                }
            }

            if (GetUserElements.AddNewItem(_context.Apiaries.Find(beehive_request.ApiaryId).UserId, "Beehive", _context) == false)
            {
                return BadRequest("Досягнута максимальна кількість вуликів для вашого тарифу");
            }

            EBeehive beehive = new EBeehive()
            {
                Name = beehive_request.Name,
                Alarm = beehive_request.Alarm,
                ApiaryId = beehive_request.ApiaryId
            };

            _context.Beehives.Add(beehive);

            await _context.SaveChangesAsync();

            //_context.Sensors.Add(new ESensor() { Min_value = 5, Max_value = 15, Is_working = true, BeehiveId = beehive.Id, Value = 10, SensorTypeId = 1 });
            //_context.Sensors.Add(new ESensor() { Min_value = 20, Max_value = 40, Is_working = true, BeehiveId = beehive.Id, Value = 31, SensorTypeId = 2 });
            //_context.Sensors.Add(new ESensor() { Min_value = 45, Max_value = 69, Is_working = true, BeehiveId = beehive.Id, Value = 55, SensorTypeId = 3 });

            //await _context.SaveChangesAsync();

            return Ok(new { beehive = beehive.Id });
        }

        // DELETE: api/Beehives/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteBeehive([FromRoute] int id)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Apiaries.Find(_context.Beehives.Find(id).ApiaryId).UserId)
            {
                return BadRequest("У цього користувача не існує такого вулика");
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EBeehive> beehives = GetUserElements.GetUserBeehives(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)), _context);
                if (beehives == null || beehives.Count == 0 || beehives.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            List<ESensor> Sensors = GetUserElements.GetUserSensorsFromBeehive(id, _context);

            if (Sensors != null && Sensors.Count > 0)
            {
                /*foreach (var s in Sensors)
                {
                    _context.Sensors.Remove(s);
                    _context.Entry(s).State = EntityState.Deleted;
                }
                await _context.SaveChangesAsync();*/

                return StatusCode(415);
            }

            var beehive = await _context.Beehives.FindAsync(id);
            if (beehive == null)
            {
                return NotFound();
            }

            _context.Beehives.Remove(beehive);
            await _context.SaveChangesAsync();

            return Ok(new { deleted = true });
        }

        private bool BeehiveExists(int id)
        {
            return _context.Beehives.Any(e => e.Id == id);
        }
    }
}
