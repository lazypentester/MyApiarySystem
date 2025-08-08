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
    public class SensorTypesController : ControllerBase
    {
        private readonly ProjectContext _context;

        public SensorTypesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/SensorTypes/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<ESensorType> GetSensorTypes()
        {
            return _context.SensorTypes;
        }

        // GET: api/SensorTypes/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSensorType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensorType = await _context.SensorTypes.FindAsync(id);

            if (sensorType == null)
            {
                return NotFound();
            }

            return Ok(sensorType);
        }

        // PUT: api/SensorTypes/edit/5
        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutSensorType([FromRoute] int id, [FromBody] ESensorType sensorType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sensorType.Id)
            {
                return BadRequest();
            }

            _context.Entry(sensorType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sensorTypeExists(id))
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

        // POST: api/SensorTypes/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostSensorType([FromBody] ESensorType sensorType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var name = new SqlParameter("name", sensorType.Name);
            var model = new SqlParameter("model", sensorType.Model);

            string str = $"Select * From SensorTypes Where [SensorTypes].[Name] = @name and [SensorTypes].[Model] = @model";

            var exist = _context.SensorTypes.FromSqlRaw(str, name, model).ToList();
            if (exist.Count != 0)
            {
                return Ok(exist);
            }

            _context.SensorTypes.Add(sensorType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensorType", new { id = sensorType.Id }, sensorType);
        }

        // DELETE: api/SensorTypes/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSensorType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensorType = await _context.SensorTypes.FindAsync(id);
            if (sensorType == null)
            {
                return NotFound();
            }

            _context.SensorTypes.Remove(sensorType);
            await _context.SaveChangesAsync();

            return Ok(sensorType);
        }

        private bool sensorTypeExists(int id)
        {
            return _context.SensorTypes.Any(e => e.Id == id);
        }
    }
}
