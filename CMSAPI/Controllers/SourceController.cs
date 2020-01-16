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
    public class SourceController : ControllerBase
    {
        //private readonly IStaffService _dataAccessProvider;
        private readonly ILogger<SourceController> _logger;
        public SourceController(ILogger<SourceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Source> Get()
        {
            var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new Source
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
