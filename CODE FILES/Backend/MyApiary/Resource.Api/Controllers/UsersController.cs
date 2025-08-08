using Hashing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Resource.Api.Confirmations;
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
    public class UsersController : ControllerBase
    {
        private readonly ProjectContext _context;

        public UsersController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<EUser> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/Users/getApiaries/2
        [HttpGet]
        [Route("getApiaries/{id}")]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<IEnumerable<EApiary>> GetUserEnterprises([FromRoute] int id)
        {
            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (Convert.ToInt32(User.FindFirstValue("Sub")) != id)
                {
                    return BadRequest("id пользователя не соответствует запрашиваемому id.");
                }
            }
            return Ok(_context.Apiaries.Where(x => x.UserId == id).ToList());
        }

        // GET: api/Users/getbyid/5
        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != id)
            {
                return BadRequest(ModelState);
            }

            EUser user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserResponce responce = new UserResponce()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.Phone,
                Mail = user.Mail,
                Tariff = _context.Tariffs.Find(user.TariffId).Name
            };

            return Ok(responce);
        }

        // PUT: api/Users/edit/
        [HttpPut]
        [Route("edit")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PutUser([FromBody] UserResponce user)
        {
            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (!ModelState.IsValid || Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) != user.Id)
                {
                    return BadRequest(ModelState);
                }
            }

            //_context.Entry(user).State = EntityState.Modified;

            EUser current_user = _context.Users.Find(user.Id);

            _context.Entry(current_user).Property(x => x.Name).CurrentValue = user.Name;
            _context.Entry(current_user).Property(x => x.Surname).CurrentValue = user.Surname;

            //if (user.Pass.ToLower() != "empty")
            //{
            //    _context.Entry(current_user).Property(x => x.Pass).CurrentValue = user.Pass;
            //    _context.Entry(current_user).Property(x => x.Name).IsModified = true;
            //}

            _context.Entry(current_user).Property(x => x.Name).IsModified = true;
            _context.Entry(current_user).Property(x => x.Surname).IsModified = true;

            //_context.Entry(current_user).Property(x => x.Pass).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Status = "Okey" });
        }

        // POST: api/Users/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostUser([FromBody] EUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mail = new SqlParameter("mail", user.Mail);
            string sql = $"Select * From Users Where [Users].[Mail] = @mail";
            var exist = _context.Users.FromSqlRaw(sql, mail).ToList();
            if (exist.Count != 0)
            {
                //return Ok(exist);
                return BadRequest("Пользователь с таким электронным адресом уже существует в базе.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // POST: api/Users/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegistertUser([FromBody] EUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mail = _context.Users.Where(x => x.Mail == user.Mail).ToList();
            if(mail.Count != 0)
                return StatusCode(411);

            var phone = _context.Users.Where(x => x.Phone == user.Phone).ToList();
            if (phone.Count != 0)
                return StatusCode(412);

            user.AccountConfirmed = false;
            user.Salt = passwordHash.generateSalt(15);
            user.Pass = passwordHash.generateHash(user.Pass, user.Salt);
            user.RoleId = _context.Roles.Where(x => x.Name == "User").Select(a => a.Id).FirstOrDefault();
            user.TariffId = _context.Tariffs.Where(x => x.Name == "Trial").Select(a => a.Id).FirstOrDefault();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.Confirmations.Add(new EConfirmation
            {
                Activated = false,
                UserId = user.Id,
                Secret_code = generateSecretCode.Generate(6)
            });
            await _context.SaveChangesAsync();

            return Ok(new { id = user.Id });
        }

        // POST: api/Users/changePassword/{user_id}
        [HttpPost]
        [Route("changepassword/{user_id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> changeUserPassword([FromRoute] int user_id, [FromBody] ChangeUserPass changePassModel)
        {
            if (!ModelState.IsValid || user_id != changePassModel.Id)
            {
                return BadRequest(ModelState);
            }

            if (User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "User")
            {
                if (Convert.ToInt32(User.FindFirstValue("Sub")) != user_id || Convert.ToInt32(User.FindFirstValue("Sub")) != changePassModel.Id)
                {
                    return BadRequest("id пользователя не соответствует запрашиваемому id.");
                }
            }

            EUser user_new_pass = _context.Users.Find(user_id);
            if (user_new_pass == null)
            {
                return BadRequest("Пользователя с данным id не существует.");
            }

            if (!passwordHash.verifyPass(changePassModel.Pass_old, user_new_pass.Salt, user_new_pass.Pass))
            {
                return BadRequest("Неверный текущий пароль.");
            }


            //errorrrrrrrrrrrrrr
            user_new_pass.Salt = passwordHash.generateSalt(15);
            user_new_pass.Pass = passwordHash.generateHash(changePassModel.Pass_new, user_new_pass.Salt);

            _context.Users.Add(user_new_pass);
            await _context.SaveChangesAsync();

            return Ok(new { id = user_new_pass.Id });
        }

        // POST: api/Users/confirmationAccountSendCode
        [HttpPost]
        [Route("confirmationAccountSendCode")]
        public IActionResult confirmationAccountSendCode([FromBody] AccountConfirmation userConfirmation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            else if (_context.Users.Where(x => x.Mail == userConfirmation.Mail).Count() == 0)
            {
                return BadRequest("Mail or Phone don`t exist in this user.");
            }

            EUser user = _context.Users.Where(x => x.Mail == userConfirmation.Mail).FirstOrDefault();
            string codeConfirm = _context.Confirmations.Where(x => x.UserId == user.Id && x.Activated == false).Select(x => x.Secret_code).FirstOrDefault();
            bool sended = false;


            if (userConfirmation.MailConfirmSelected)
            {
                try
                {
                    sended = Mail.sendEmail(user.Mail, codeConfirm);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
            } 
            else
            {
                try
                {
                    sended = Phone.SendSms(user.Phone, codeConfirm);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
            }

            return Ok(new { sended = sended });
        }

        // POST: api/Users/confirmationAccountCheckCode
        [HttpPost]
        [Route("confirmationAccountCheckCode")]
        public async Task<IActionResult> confirmationAccountCheckCode([FromBody] AccountConfirmationCode userConfirmationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (_context.Users.Where(x => x.Mail == userConfirmationCode.Mail).Count() == 0)
            {
                return BadRequest("Mail or Phone don`t exist in this user.");
            }
            else if (!userConfirmationCode.MailConfirmed && !userConfirmationCode.PhoneConfirmed)
            {
                return BadRequest("MailConfirmed and PhoneConfirmed === false");
            }

            bool confirmed = false;

            EUser user = _context.Users.Where(x => x.Mail == userConfirmationCode.Mail).FirstOrDefault();
            string codeConfirm = _context.Confirmations.Where(x => x.UserId == user.Id && x.Activated == false).Select(x => x.Secret_code).FirstOrDefault();
            if (codeConfirm == userConfirmationCode.Code)
            {
                confirmed = true;

                if(userConfirmationCode.MailConfirmed)
                {
                    _context.Entry(user).Property(x => x.MailConfirmed).CurrentValue = true;
                    _context.Entry(user).Property(x => x.MailConfirmed).IsModified = true;
                }
                else if(userConfirmationCode.PhoneConfirmed)
                {
                    _context.Entry(user).Property(x => x.PhoneConfirmed).CurrentValue = true;
                    _context.Entry(user).Property(x => x.PhoneConfirmed).IsModified = true;
                }

                _context.Entry(user).Property(x => x.AccountConfirmed).CurrentValue = true;
                _context.Entry(user).Property(x => x.AccountConfirmed).IsModified = true;

                List<EConfirmation> confirmations = _context.Confirmations.Where(x => x.UserId == user.Id).ToList();
                _context.Confirmations.RemoveRange(confirmations);

                await _context.SaveChangesAsync();
                return Ok(new { confirmed = confirmed });
            }

            return StatusCode(402);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
