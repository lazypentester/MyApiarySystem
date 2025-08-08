using Hashing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resource.Api.Database;
using Resource.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Rrt_blacklistController : ControllerBase
    {
        private readonly ProjectContext _context;

        public Rrt_blacklistController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Rrt_blacklist/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<ERrt_blacklist> GetRrt_blacklists()
        {
            return _context.Rrt_Blacklists;
        }

        // GET: api/Rrt_blacklist/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetRrt_blacklist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ERrt_blacklist> rrt_Blacklists = GetUserElements.GetUserRrt_blacklists(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (rrt_Blacklists == null || rrt_Blacklists.Count == 0 || rrt_Blacklists.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var rrt_blacklist = await _context.Rrt_Blacklists.FindAsync(id);

            if (rrt_blacklist == null)
            {
                return NotFound();
            }

            return Ok(rrt_blacklist);
        }

        // PUT: api/Rrt_blacklist/edit/5
        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PutRrt_blacklist([FromRoute] int id, [FromBody] ERrt_blacklist rrt_Blacklist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rrt_Blacklist.Id)
            {
                return BadRequest();
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ERrt_blacklist> rrt_Blacklists = GetUserElements.GetUserRrt_blacklists(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (rrt_Blacklists == null || rrt_Blacklists.Count == 0 || rrt_Blacklists.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            _context.Entry(rrt_Blacklist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Rrt_blacklistExists(id))
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

        // POST: api/Rrt_blacklist/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PostRrt_blacklist([FromBody] ERrt_blacklist rrt_Blacklist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (Convert.ToInt32(User.FindFirstValue("Sub")) != _context.Devices.Find(rrt_Blacklist.DeviceId).UserId)
                {
                    return BadRequest("Stop hacking pls...");
                }
            }

            rrt_Blacklist.Refresh_token = tokenHash.CreateMD5(rrt_Blacklist.Refresh_token);
            _context.Rrt_Blacklists.Add(rrt_Blacklist);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRrt_blacklist", new { id = rrt_Blacklist.Id }, rrt_Blacklist);
        }

        // DELETE: api/Rrt_blacklist/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteRrt_blacklist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ERrt_blacklist> rrt_Blacklists = GetUserElements.GetUserRrt_blacklists(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (rrt_Blacklists == null || rrt_Blacklists.Count == 0 || rrt_Blacklists.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var rrt_Blacklist = await _context.Rrt_Blacklists.FindAsync(id);
            if (rrt_Blacklist == null)
            {
                return NotFound();
            }

            _context.Rrt_Blacklists.Remove(rrt_Blacklist);
            await _context.SaveChangesAsync();

            return Ok(rrt_Blacklist);
        }

        private bool Rrt_blacklistExists(int id)
        {
            return _context.Rrt_Blacklists.Any(e => e.Id == id);
        }
    }
}
