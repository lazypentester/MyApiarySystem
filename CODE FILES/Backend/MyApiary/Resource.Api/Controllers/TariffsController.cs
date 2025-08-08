using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Resource.Api.Entities;
using Resource.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public TariffsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Tariffs/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin, User")]
        public IEnumerable<ETariff> GetTariffs()
        {
            return _context.Tariffs;
        }

        // GET: api/Tariffs/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetTariff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tariff = await _context.Tariffs.FindAsync(id);

            if (tariff == null)
            {
                return NotFound();
            }

            return Ok(tariff);
        }

        // PUT: api/Tariffs/edit/5
        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTariff([FromRoute] int id, [FromBody] ETariff tariff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tariff.Id)
            {
                return BadRequest();
            }

            _context.Entry(tariff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TariffExists(id))
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

        // POST: api/Tariffs/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostTariff([FromBody] ETariff tariff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var name = new SqlParameter("name", tariff.Name);
            var max_apiaries = new SqlParameter("max_apiaries", tariff.Max_apiaries);
            var max_beehives = new SqlParameter("max_beehives", tariff.Max_beehives);
            var price = new SqlParameter("price", tariff.Price);

            string str = $"Select * From Tariffs Where [Tariffs].[Name] = @name and " +
                $"[Tariffs].[Max_apiaries] = @max_apiaries and " +
                $"[Tariffs].[Max_beehives] = @max_beehives and " +
                $"[Tariffs].[Price] = @price";

            var exist = _context.Tariffs.FromSqlRaw(str, name, max_apiaries, max_beehives, price).ToList();
            if (exist.Count != 0)
            {
                return Ok(exist);
            }

            _context.Tariffs.Add(tariff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTariff", new { id = tariff.Id }, tariff);
        }

        // DELETE: api/Tariffs/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTariff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tariff = await _context.Tariffs.FindAsync(id);
            if (tariff == null)
            {
                return NotFound();
            }

            _context.Tariffs.Remove(tariff);
            await _context.SaveChangesAsync();

            return Ok(tariff);
        }

        private bool TariffExists(int id)
        {
            return _context.Tariffs.Any(e => e.Id == id);
        }
    }
}
