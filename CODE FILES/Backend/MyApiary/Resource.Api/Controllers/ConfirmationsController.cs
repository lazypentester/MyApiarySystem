using Hashing;
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
    public class ConfirmationsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ConfirmationsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Confirmations/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<EConfirmation> GetConfirmations()
        {
            return _context.Confirmations;
        }

        // GET: api/Confirmations/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetConfirmation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EConfirmation> confirmations = GetUserElements.GetUserConfirmations(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (confirmations == null || confirmations.Count == 0 || confirmations.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var confirmation = await _context.Confirmations.FindAsync(id);

            if (confirmation == null)
            {
                return NotFound();
            }

            return Ok(confirmation);
        }

        // PUT: api/Confirmations/edit/5
        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PutConfirmation([FromRoute] int id, [FromBody] EConfirmation confirmation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != confirmation.Id)
            {
                return BadRequest();
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EConfirmation> confirmations = GetUserElements.GetUserConfirmations(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (confirmations == null || confirmations.Count == 0 || confirmations.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            _context.Entry(confirmation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfirmationExists(id))
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

        // POST: api/Confirmations/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PostConfirmation([FromBody] EConfirmation confirmation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (Convert.ToInt32(User.FindFirstValue("Sub")) != _context.Confirmations.Find(confirmation.Id).UserId)
                {
                    return BadRequest("Stop hacking pls...");
                }
            }

            confirmation.Secret_code = generateSecretCode.Generate(6);
            _context.Confirmations.Add(confirmation);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfirmation", new { id = confirmation.Id }, confirmation);
        }

        // DELETE: api/Confirmations/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteConfirmation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<EConfirmation> confirmations = GetUserElements.GetUserConfirmations(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (confirmations == null || confirmations.Count == 0 || confirmations.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var confirmation = await _context.Confirmations.FindAsync(id);
            if (confirmation == null)
            {
                return NotFound();
            }

            _context.Confirmations.Remove(confirmation);
            await _context.SaveChangesAsync();

            return Ok(confirmation);
        }

        // POST: api/Confirmations/send_mail/user_id
        [HttpPost]
        [Route("send_mail/{id}")]
        public ActionResult SendConfirmationMail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Confirmations.Where(x => x.UserId == id && !x.Activated).Count() == 0)
            {
                return BadRequest("Stop hacking pls...");
            }

            Mail.sendEmail(_context.Users.Find(id).Mail, _context.Confirmations.Where(x => x.UserId == id && !x.Activated).FirstOrDefault().Secret_code);

            return Ok();
        }

        // POST: api/Confirmations/send_phone/user_id
        [HttpPost]
        [Route("send_phone/{id}")]
        public ActionResult SendConfirmationPhone([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Confirmations.Where(x => x.UserId == id && !x.Activated).Count() == 0)
            {
                return BadRequest("Stop hacking pls...");
            }

            Phone.SendSms(_context.Users.Find(id).Phone, _context.Confirmations.Where(x => x.UserId == id && !x.Activated).FirstOrDefault().Secret_code);

            return Ok();
        }

        // POST: api/Confirmations/validation_code/user_id
        [HttpPost]
        [Route("validation_code/{id}")]
        public async Task<IActionResult> ValidationSecretCode([FromRoute] int id, [FromBody] RegisterValidation registerValidation)
        {
            if (!ModelState.IsValid || registerValidation.Id != id)
            {
                return BadRequest(ModelState);
            }

            if (_context.Confirmations.Where(x => x.UserId == id && !x.Activated).Count() == 0)
            {
                return BadRequest("Stop hacking pls...");
            }

            EConfirmation confirmation = _context.Confirmations.Where(x => x.UserId == id && !x.Activated).FirstOrDefault();
            EUser user = _context.Users.Find(id);

            if (confirmation.Secret_code.Equals(registerValidation.secretCode))
            {
                //confirmation.Activated = true;
                _context.Entry(confirmation).Property(x => x.Activated).CurrentValue = true;
                _context.Entry(confirmation).Property(x => x.Activated).IsModified = true;

                _context.Entry(user).Property(x => x.AccountConfirmed).CurrentValue = true;
                _context.Entry(user).Property(x => x.AccountConfirmed).IsModified = true;

                await _context.SaveChangesAsync();
                return Ok("Аккаунт подтвержден.");
            }

            return BadRequest("Неверный код подтверждения.");
        }

        private bool ConfirmationExists(int id)
        {
            return _context.Confirmations.Any(e => e.Id == id);
        }
    }
}
