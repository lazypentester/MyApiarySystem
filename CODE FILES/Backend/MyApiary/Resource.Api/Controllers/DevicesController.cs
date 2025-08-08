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
    public class DevicesController : ControllerBase
    {
        private readonly ProjectContext _context;

        public DevicesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Devices/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<EDevice> GetDevices()
        {
            return _context.Devices;
        }

        // GET: api/Devices/getallbyuser/user_id
        [HttpGet]
        [Route("getallbyuser/{user_id}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetDevicesByIdUser([FromRoute] int user_id)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != user_id)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EDevice> devices = GetUserElements.GetUserDevices(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)), _context);
                if (devices == null || devices.Count == 0 || devices.Where(x => x.UserId == user_id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            List<DevicesResponce> devicesResponce = new List<DevicesResponce>();

            foreach(var device in _context.Devices.Where(x => x.UserId == user_id))
            {
                devicesResponce.Add(new DevicesResponce()
                {
                    Id = device.Id,
                    Name = device.Name,
                    Mac_address = device.Mac_address,
                    Fingerprint = device.Fingerprint
                });
            }

            return Ok(devicesResponce);
        }

        // GET: api/Devices/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EDevice> devices = GetUserElements.GetUserDevices(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (devices == null || devices.Count == 0 || devices.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // PUT: api/Devices/edit/5
        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PutDevice([FromRoute] int id, [FromBody] EDevice device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.Id)
            {
                return BadRequest();
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EDevice> devices = GetUserElements.GetUserDevices(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (devices == null || devices.Count == 0 || devices.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        // POST: api/Devices/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PostDevice([FromBody] EDevice device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (Convert.ToInt32(User.FindFirstValue("Sub")) != _context.Devices.Find(device.Id).UserId)
                {
                    return BadRequest("Stop hacking pls...");
                }
            }

            _context.Devices.Add(device);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }

        // DELETE: api/Devices/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EDevice> devices = GetUserElements.GetUserDevices(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (devices == null || devices.Count == 0 || devices.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return Ok(device);
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}
