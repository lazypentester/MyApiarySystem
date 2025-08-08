using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Resource.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ProjectContext _context;

        public RolesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Roles/test
        [HttpGet]
        [Route("test")]
        public IEnumerable<string[]> TestRequest()
        {
            return new string[][] { new string[] { "1", "2", "3", "4", "5" }, new string[] { "1", "2", "3", "4", "5" } };
        }

        // GET: api/Roles/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<ERole> GetRoles()
        {
            return _context.Roles;
        }

        // GET: api/Roles/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/Roles/edit/5
        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRole([FromRoute] int id, [FromBody] ERole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostRole([FromBody] ERole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var name = new SqlParameter("name", role.Name);

            string str = $"Select * From Roles Where [Roles].[Name] = @name";

            var exist = _context.Roles.FromSqlRaw(str, name).ToList();
            if (exist.Count != 0)
            {
                return Ok(exist);
            }

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(role);
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
