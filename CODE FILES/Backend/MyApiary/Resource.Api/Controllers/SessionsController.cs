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

namespace Resource.Api.Confirmations
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public SessionsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Sessions/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<ESession> GetSessions()
        {
            return _context.Sessions;
        }

        // GET: api/Sessions/getallbydevice/device_id
        [HttpGet]
        [Route("getallbydevice/{device_id}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetSessionsByDevice([FromRoute] int device_id)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != _context.Devices.Find(device_id).UserId)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ESession> sessions = GetUserElements.GetUserSessions(device_id, _context);
                if (sessions == null || sessions.Count == 0 || sessions.Where(x => x.DeviceId == device_id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            List<SessionsResponce> sessionsResponce = new List<SessionsResponce>();

            foreach (var session in _context.Sessions.Where(x => x.DeviceId == device_id))
            {
                sessionsResponce.Add(new SessionsResponce()
                {
                    Id = session.Id,
                    Start_date = session.Start_date,
                    Ip_address = session.Ip_address,
                    Geolocation = session.Geolocation,
                    DeviceId = session.DeviceId
                });
            }

            return Ok(sessionsResponce);
        }

        // GET: api/Sessions/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetSession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ESession> sessions = GetUserElements.GetUserSessions(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (sessions == null || sessions.Count == 0 || sessions.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return Ok(session);
        }

        // PUT: api/Sessions/edit/5
        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PutSession([FromRoute] int id, [FromBody] ESession session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != session.Id)
            {
                return BadRequest();
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ESession> sessions = GetUserElements.GetUserSessions(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (sessions == null || sessions.Count == 0 || sessions.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sessions/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PostSession([FromBody] ESession session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (Convert.ToInt32(User.FindFirstValue("Sub")) != _context.Devices.Find(session.DeviceId).UserId)
                {
                    return BadRequest("Stop hacking pls...");
                }
            }

            _context.Sessions.Add(session);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSession", new { id = session.Id }, session);
        }

        // DELETE: api/Sessions/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteSession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ESession> sessions = GetUserElements.GetUserSessions(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (sessions == null || sessions.Count == 0 || sessions.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return Ok(session);
        }

        /////////////////////// ERRRORRRR из-за двойного фромбоди - решение - обьеденить 2 модели в одну и из неё уже доставать данные

        // POST: api/Sessions/createSession/user_id
        [HttpPost]
        [Route("createSession/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreateNewSession([FromRoute] int id, [FromBody] CreateSession session_request)
        {
            if (!ModelState.IsValid || id != Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return BadRequest(ModelState);
            }

            EDevice device = _context.Devices.Where(x => x.UserId == id && x.Fingerprint == session_request.Device_Fingerprint).FirstOrDefault();
            ESession session;
            ERrt_blacklist rrt_Blacklist;

            if (device != null)
            {
                rrt_Blacklist = new ERrt_blacklist()
                {
                    DeviceId = device.Id,
                    Refresh_token = device.Refresh_token
                };

                _context.Entry(device).Property(x => x.Refresh_token).CurrentValue = session_request.Device_Refresh_token;
                _context.Entry(device).Property(x => x.Refresh_token).IsModified = true;

                _context.Rrt_Blacklists.Add(rrt_Blacklist);
                await _context.SaveChangesAsync();
            }
            else
            {
                device = new EDevice()
                {
                    Name = session_request.Device_Name,
                    Mac_address = session_request.Device_Mac_address,
                    Fingerprint = session_request.Device_Fingerprint,
                    Refresh_token = session_request.Device_Refresh_token,
                    UserId = id
                };

                _context.Devices.Add(device);
                await _context.SaveChangesAsync();
            }

            session = new ESession()
            {
                DeviceId = device.Id,
                Start_date = DateTime.Now,
                Ip_address = session_request.Session_Ip_address,
                Geolocation = session_request.Session_Geolocation,
            };
            _context.Sessions.Add(session);

            await _context.SaveChangesAsync();

            session_request.Session_Id = session.Id;
            session_request.Device_id = device.Id;

            return Ok(session_request);
        }

        // POST: api/Sessions/continueSession/user_id
        [HttpPost]
        [Route("continueSession/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ContinueSession([FromRoute] int user_id, [FromBody] int device_id, string refresh_token_new)
        {
            if (!ModelState.IsValid || user_id != Convert.ToInt32(User.FindFirstValue("Sub")) || _context.Devices.Where(x => x.UserId == user_id && x.Id == device_id).Count() == 0)
            {
                return BadRequest(ModelState);
            }

            EDevice device = _context.Devices.Find(device_id);
            ERrt_blacklist rrt_Blacklist = new ERrt_blacklist()
            {
                DeviceId = device_id,
                Refresh_token = device.Refresh_token
            };

            _context.Entry(device).Property(x => x.Refresh_token).CurrentValue = refresh_token_new;
            _context.Entry(device).Property(x => x.Refresh_token).IsModified = true;

            _context.Rrt_Blacklists.Add(rrt_Blacklist);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
