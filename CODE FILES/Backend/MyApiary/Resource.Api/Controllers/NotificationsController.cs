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
    public class NotificationsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public NotificationsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Notifications/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<ENotification> GetNotifications()
        {
            return _context.Notifications;
        }

        // GET: api/Notifications/getallbyapiary/5
        [HttpGet]
        [Route("getallbyapiary/{apiary_id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetNotificationByApiary([FromRoute] int apiary_id)
        {
            if (!ModelState.IsValid || _context.Apiaries.Find(apiary_id).UserId != Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return BadRequest(ModelState);
            }

            List<ENotification> e_notifications = new List<ENotification>();
            e_notifications.AddRange(await _context.Notifications.Where(x => x.ApiaryId == apiary_id).ToListAsync());

            if (e_notifications == null)
            {
                return NotFound();
            }

            List<NotofocationsResponceByApiary> responceByApiaries = new List<NotofocationsResponceByApiary>();
            foreach(var note in e_notifications)
            {
                responceByApiaries.Add(new NotofocationsResponceByApiary()
                {
                    Id = note.Id,
                    Name = note.Name,
                    Description = note.Description,
                    Created_date = note.Created_date,
                    Readed = note.Readed,
                    ApiaryId = note.ApiaryId
                });
            }


            return Ok(responceByApiaries);
        }

        // PUT: api/Notifications/setreaded/5
        [HttpPut]
        [Route("setreaded/{notification_id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateNotificationById([FromRoute] int notification_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ENotification eNotification = _context.Notifications.Find(notification_id);

            EApiary eApiary = _context.Apiaries.Find(eNotification.ApiaryId);
            EUser eUser = _context.Users.Find(eApiary.UserId);

            if (eNotification == null || eApiary == null || eUser == null || eUser.Id != Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return NotFound();
            }

            _context.Entry(eNotification).Property(x => x.Readed).CurrentValue = true;
            _context.Entry(eNotification).Property(x => x.Read_date).CurrentValue = DateTime.Now;

            _context.Entry(eNotification).Property(x => x.Readed).IsModified = true;
            _context.Entry(eNotification).Property(x => x.Read_date).IsModified = true;

            _context.Entry(eNotification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

            return Ok(new { readed_notification = true });
        }

        // GET: api/Notifications/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetNotification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ENotification> notifications = GetUserElements.GetUserNotifications(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (notifications == null || notifications.Count == 0 || notifications.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // PUT: api/Notifications/edit/5
        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PutNotification([FromRoute] int id, [FromBody] ENotification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notification.Id)
            {
                return BadRequest();
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ENotification> notifications = GetUserElements.GetUserNotifications(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (notifications == null || notifications.Count == 0 || notifications.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // POST: api/Notifications/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PostNotification([FromBody] ENotification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (Convert.ToInt32(User.FindFirstValue("Sub")) != _context.Apiaries.Find(notification.ApiaryId).UserId)
                {
                    return BadRequest("Stop hacking pls...");
                }
            }

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
        }

        // DELETE: api/Notification/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteNotification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                List<ENotification> notifications = GetUserElements.GetUserNotifications(Convert.ToInt32(User.FindFirstValue("Sub")), _context);
                if (notifications == null || notifications.Count == 0 || notifications.Where(x => x.Id == id).ToList().Count == 0)
                {
                    return NotFound();
                }
            }

            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok(notification);
        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}
