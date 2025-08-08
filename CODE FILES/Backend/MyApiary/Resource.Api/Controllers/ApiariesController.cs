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
    public class ApiariesController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ApiariesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Apiaries/getbeehives/4
        [HttpGet]
        [Route("getBeehives/{id}")]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<IEnumerable<EBeehive>> GetApiaryBeehives([FromRoute] int id)
        {
            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
                {
                    List<EApiary> apiaries = GetUserElements.GetUserApiaries(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                    //Good version 
                    //apiaries = _context.Apiaries.Where(x => x.UserId == Convert.ToInt32(User.FindFirstValue("Sub"))).ToList();
                    if (apiaries == null || apiaries.Count == 0 || apiaries.Where(x => x.Id == id).ToList().Count == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return _context.Beehives.Where(x => x.ApiaryId == id).ToList();
        }

        // GET: api/Apiaries/getallbyuserid/user_id
        [HttpGet]
        [Route("getallbyuserid/{user_id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetApiariesByUserId([FromRoute] int user_id)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != user_id)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EApiary> apiaries = GetUserElements.GetUserApiaries(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)), _context);
                if (apiaries == null || apiaries.Count == 0 || apiaries.Where(x => x.UserId == user_id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var apiaries_list = await _context.Apiaries.Where(x => x.UserId == user_id).ToListAsync();

            if (apiaries_list == null)
            {
                return NotFound();
            }

            bool ConnectionWire;
            bool ConnectionWireless;
            int CountOfBeehives = 0;
            bool Notifications;

            List<Apiary> responce = new List<Apiary>();

            foreach(var item in apiaries_list)
            {
                try
                {
                    if (GetUserBaseStations(item).Count == 0)
                        ConnectionWire = false;
                    else
                        ConnectionWire = GetUserBaseStations(item).Where(x => !x.Is_working).Count() == 0 ? true : false;

                    if (GetApiarySensors(item).Count == 0)
                        ConnectionWireless = false;
                    else
                        ConnectionWireless = GetApiarySensors(item).Where(x => !x.Is_working).Count() == 0 ? true : false;

                    CountOfBeehives = _context.Beehives.Where(x => x.ApiaryId == item.Id).Count();
                    Notifications = _context.Notifications.Where(x => x.ApiaryId == item.Id && !x.Readed).Count() == 0 ? false : true;
                } catch(Exception e)
                {
                    return BadRequest(e.Message);
                }

                responce.Add(new Apiary()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    ConnectionWire = ConnectionWire,
                    ConnectionWireless = ConnectionWireless,
                    CountOfBeehives = CountOfBeehives,
                    Notifications = Notifications
                });
            }

            return Ok(responce);
        }

        private List<ESensor> GetApiarySensors(EApiary apiary)
        {
            List<EBeehive> beehives = new List<EBeehive>();
            List<ESensor> sensors = new List<ESensor>();

            if (apiary != null)
            {
                beehives.AddRange(_context.Beehives.Where(x => x.ApiaryId == apiary.Id).ToList());
                foreach (var beehive in beehives) sensors.AddRange(_context.Sensors.Where(x => x.BeehiveId == beehive.Id).ToList());
            }

            return sensors;
        }

        private List<EBaseStation> GetUserBaseStations(EApiary apiary)
        {
            List<EBeehive> beehives = new List<EBeehive>();
            List<ESensor> sensors = new List<ESensor>();
            List<EBaseStation> stations = new List<EBaseStation>();

            if (apiary != null)
            {
                beehives.AddRange(_context.Beehives.Where(x => x.ApiaryId == apiary.Id).ToList());
                foreach (var beehive in beehives) sensors.AddRange(_context.Sensors.Where(x => x.BeehiveId == beehive.Id).ToList());
                foreach (var sensor in sensors) stations.AddRange(_context.BaseStations.Where(x => x.Id == sensor.BaseStationId).ToList());
            }

            return stations;
        }

        // GET: api/Apiaries/getall
        [HttpGet]
        [Route("getAll")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<EApiary> GetApiaries()
        {
            return _context.Apiaries;
        }

        // GET: api/Apiaries/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetApiary([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EApiary> apiaries = GetUserElements.GetUserApiaries(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (apiaries == null || apiaries.Count == 0 || apiaries.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var apiary = await _context.Apiaries.FindAsync(id);

            if (apiary == null)
            {
                return NotFound();
            }

            return Ok(apiary);
        }

        // PUT: api/Apiaries/edit/5
        [HttpPut]
        [Route("edit/{apiary_id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PutApiaries([FromRoute] int apiary_id, [FromBody] ApiaryUpdate apiary_update)
        {
            if (!ModelState.IsValid || apiary_id != apiary_update.Id || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Apiaries.Find(apiary_id).UserId)
            {
                return BadRequest(ModelState);
            }

            EApiary eApiary = _context.Apiaries.Find(apiary_id);
            
            if(eApiary == null)
            {
                return NotFound();
            }

            _context.Entry(eApiary).Property(x => x.Name).CurrentValue = apiary_update.Name;
            _context.Entry(eApiary).Property(x => x.Address).CurrentValue = apiary_update.Address;

            _context.Entry(eApiary).Property(x => x.Name).IsModified = true;
            _context.Entry(eApiary).Property(x => x.Address).IsModified = true;

            _context.Entry(eApiary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

            return Ok(new { updated_apiary = true });
        }

        // POST: api/Apiaries/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PostApiary([FromBody] AddNewApiary apiary_request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != apiary_request.UserId)
                {
                    return BadRequest("Stop hacking pls...");
                }
            }

            if (GetUserElements.AddNewItem(apiary_request.UserId, "Apiary", _context) == false)
            {
                return BadRequest(new { error = "Достигнуто максимально кол-во Apiearies для вашего тарифа." });
            }

            EApiary apiary = new EApiary()
            {
                Name = apiary_request.Name,
                Address = apiary_request.Address,
                UserId = apiary_request.UserId
            };

            _context.Apiaries.Add(apiary);
            await _context.SaveChangesAsync();

            return Ok(new { apiary_id = apiary.Id });
        }

        // DELETE: api/Apiaries/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteApiary([FromRoute] int id)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Apiaries.Find(id).UserId)
            {
                return BadRequest("У цього користувача не існує такої пасіки");
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EApiary> apiaries = GetUserElements.GetUserApiaries(_context.Apiaries.Find(id).UserId, _context);
                if (apiaries == null || apiaries.Count == 0 || apiaries.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            List<EBeehive> beehives = _context.Beehives.Where(x => x.ApiaryId == id).ToList();

            List<ESensor> Sensors = new List<ESensor>();

            foreach (EBeehive behive in beehives)
            {
                Sensors.AddRange(GetUserElements.GetUserSensorsFromBeehive(behive.Id, _context));
            }

            if (Sensors != null && Sensors.Count > 0)
            {
                /*foreach (var s in Sensors)
                {
                    _context.Sensors.Remove(s);
                    _context.Entry(s).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();
                }*/

                return StatusCode(415);
            }

            /*if (beehives != null && beehives.Count > 0)
            {
                foreach (var beehive in beehives)
                {
                    _context.Beehives.Remove(beehive);
                    _context.Entry(beehive).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();
                }
            }*/

            var apiary = await _context.Apiaries.FindAsync(id);
            if (apiary == null)
            {
                return NotFound();
            }

            _context.Apiaries.Remove(apiary);
            _context.Entry(apiary).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return Ok(new { deleted = true });
        }

        private bool ApiaryExists(int id)
        {
            return _context.Apiaries.Any(e => e.Id == id);
        }
    }
}
