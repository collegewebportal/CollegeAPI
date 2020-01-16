using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LeadController : ControllerBase
    {
        //private readonly IStaffService _dataAccessProvider;
        private readonly ILogger<LeadController> _logger;

        public LeadController(ILogger<LeadController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Leads> Get()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new Leads
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            return null;
        }
    }
}
