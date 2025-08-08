using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseStationsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public BaseStationsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Basestations/getall
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<EBaseStation> GetUsers()
        {
            return _context.BaseStations;
        }
    }
}
