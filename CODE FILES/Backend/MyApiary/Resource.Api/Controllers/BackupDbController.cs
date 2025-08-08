using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Resource.Api.Database;
using Resource.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupDbController : ControllerBase
    {
        // CreateBackUp: api/BackupDb/create
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBackUp()
        {
            //using (SqlConnection conn = new SqlConnection(Connection.))
            //{
                SqlCommand command;
                string destdir = @"C:\backupdb";
                if (!Directory.Exists(destdir))
                {
                    Directory.CreateDirectory(destdir);
                }
                try
                {
                    Connection.Open();
                    command = new SqlCommand($@"backup database API_APZ2 to disk='{destdir}\{DateTime.Now.ToString("ddMMyyyy_HHmmss")}.Bak'", Connection.Get());
                    int execute = await command.ExecuteNonQueryAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            //}
        }


        // Get all BackUps: api/BackupDb/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> getAllBackUps()
        {
            string destdir = @"C:\backupdb";
            string[] files = Directory.GetFiles(destdir);
            Backups backups = new Backups();
            foreach (string file in files)
            {
                var arr = file.Split(@"\");
                string name = arr[arr.Length - 1];
                backups.List.Add(name);
            }

            return backups.List;
        }


        // ReturnToBackUp: api/BackupDb/return
        [HttpPost]
        [Route("return")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReturnToBackUp([FromBody] JObject obj)
        {
            //using (SqlConnection conn = new SqlConnection(ConnectionString.str))
            //{
                SqlCommand command;
                string name = obj["name"].ToString();

                try
                {
                    Connection.Open();
                    string destdir = @"C:\backupdb";
                    command = new SqlCommand($@"Restore database API_APZ2 from disk='{destdir}\{name}' With Replace", Connection.Get());
                    int execute = await command.ExecuteNonQueryAsync();
                    return Ok("Database restored successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            //}
        }
    }
}
