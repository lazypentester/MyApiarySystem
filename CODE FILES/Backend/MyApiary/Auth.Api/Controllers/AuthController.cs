using Auth.Api.Database;
using Auth.Api.Models;
using Auth.Api.Tokens;
using Auth.Common;
using Hashing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> authOptions;

        public AuthController(IOptions<AuthOptions> authOptions)
        {
            this.authOptions = authOptions;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Login request)
        {

            Account user = Authenticate(request.Email, request.Password);

            if (user == null)
                return NotFound(new { error = "Not found user" });
            else if(!user.Confirmed && user.Role != "Admin")
                return StatusCode(410);

            Tariff tariff = getTariff(user.TariffId);

            if (user != null && tariff != null)
            {
                var tokenShort = AccessToken.GenerateJWT(user, authOptions.Value.TokenLifetime, authOptions);
                var tokenLong = AccessToken.GenerateJWT(user, authOptions.Value.TokenLifetime * 6, authOptions);

                return Ok(new
                {
                    access_token_short = tokenShort,
                    access_token_long = tokenLong,
                    user_id = user.Id,
                    user.Name,
                    user.Surname,
                    user.Phone,
                    user.Mail,
                    user.TariffId,
                    tariff_name = tariff.Name,
                    tariff.Max_apiaries,
                    tariff.Max_beehives,
                    tariff.Price
                });
            }

            return Unauthorized("Wrong E-mail or password.");
        }

        private Account Authenticate(string email, string pass)
        {
            Account account = null;

            Connection.Open();

            SqlParameter mail = new SqlParameter("Email", SqlDbType.NVarChar);
            SqlCommand getUser = new SqlCommand("Select * From Users Where Mail = @Email", Connection.Get());

            getUser.Parameters.Add(mail);

            getUser.Parameters["Email"].SqlValue = email;

            SqlDataReader reader = getUser.ExecuteReader();
            int rl_id = 0;

            string pass_salt = "";
            string pass_hash = "";

            while (reader.Read())
            {
                account = new Account()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Mail = reader["Mail"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    TariffId = Convert.ToInt32(reader["TariffId"]),
                    Confirmed = Convert.ToBoolean(reader["AccountConfirmed"])
                };

                rl_id = Convert.ToInt32(reader["RoleId"]);

                pass_salt = reader["Salt"].ToString();
                pass_hash = reader["Pass"].ToString();
            }

            if (reader != null && !reader.IsClosed)
                reader.Close();

            if (account != null && passwordHash.verifyPass(pass, pass_salt, pass_hash))
            {
                account.Role = GetAccountRole(rl_id);
            } 
            else
            {
                Connection.Close();
                return null;
            }

            Connection.Close();

            return account;
        }

        private Tariff getTariff(int tariff_id)
        {
            Tariff tariff = null;

            Connection.Open();

            SqlParameter id = new SqlParameter("Id", SqlDbType.Int);
            SqlCommand getTariff = new SqlCommand("Select * From Tariffs Where Id = @Id", Connection.Get());
            getTariff.Parameters.Add(id);
            getTariff.Parameters["Id"].SqlValue = tariff_id;
            SqlDataReader reader = getTariff.ExecuteReader();

            while (reader.Read())
            {
                tariff = new Tariff()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Max_apiaries = Convert.ToInt32(reader["Max_apiaries"]),
                    Max_beehives = Convert.ToInt32(reader["Max_beehives"]),
                    Price = Convert.ToDecimal(reader["Price"])
                };
            }

            if (reader != null && !reader.IsClosed)
                reader.Close();

            Connection.Close();

            if (tariff == null)
            {
                return null;
            }

            return tariff;
        }

        [HttpPost]
        [Route("updateTokens/{user_id}")]
        public IActionResult updateTokensMethod([FromRoute] int user_id, [FromBody] string rr_token)
        {

            Account user = AuthenticateByRefreshToken(user_id, rr_token);

            if (user == null)
                return Unauthorized("Wrong Refresh token.");
                
            var tokenShort = AccessToken.GenerateJWT(user, authOptions.Value.TokenLifetime, authOptions);
            var tokenLong = AccessToken.GenerateJWT(user, authOptions.Value.TokenLifetime * 6, authOptions);

            return Ok(new
            {
                access_token_short = tokenShort,
                access_token_long = tokenLong,
                user_id = user.Id,
                user.Mail
            });
        }

        private Account AuthenticateByRefreshToken(int user_id, string rr_token)
        {
            Account account = null;

            Connection.Open();

            SqlParameter Usr_id = new SqlParameter("User_id", SqlDbType.Int);
            SqlParameter Rr_t = new SqlParameter("Rr_token", SqlDbType.NVarChar);

            SqlCommand getUser = new SqlCommand("Select [User].[Id], [User].[Mail], [User].[RoleId] From Users, Devices " +
                "Where [User].[Id] = @User_id and [Devices].[UserId] = [User].[Id] and [Devices].[Refresh_token] = @Rr_token", Connection.Get());

            getUser.Parameters.AddRange(new SqlParameter[] { Usr_id, Rr_t });

            getUser.Parameters["User_id"].SqlValue = user_id;
            getUser.Parameters["Rr_token"].SqlValue = rr_token;

            SqlDataReader reader = getUser.ExecuteReader();
            int rl_id = 0;

            while (reader.Read())
            {
                account = new Account()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Mail = reader["Mail"].ToString(),
                };

                rl_id = Convert.ToInt32(reader["RoleId"]);
            }

            if (reader != null && !reader.IsClosed)
                reader.Close();

            if (account != null)
            {
                account.Role = GetAccountRole(rl_id);
            }
            else
            {
                Connection.Close();
                return null;
            }

            Connection.Close();

            return account;
        }

        private string GetAccountRole(int rl_id)
        {
            Connection.Open();
            SqlParameter role_id = new SqlParameter("RoleId", SqlDbType.Int);
            SqlCommand get_role = new SqlCommand("Select Name from Roles Where Id = @RoleId", Connection.Get());
            get_role.Parameters.Add(role_id);
            get_role.Parameters["RoleId"].Value = rl_id;
            SqlDataReader role_reader = get_role.ExecuteReader();

            string role_name = "";

            while (role_reader.Read())
            {
                role_name = role_reader["Name"].ToString();
            }

            if (role_reader != null && !role_reader.IsClosed)
                role_reader.Close();

            Connection.Close();

            return role_name;
        }
    }
}
