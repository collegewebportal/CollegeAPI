using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;

namespace CMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IStaffService _dataAccessProvider;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IStaffService dataAccessProvider ,ILogger<AuthController> logger)
        {
            _logger = logger;
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<bool>> Login( string id,string pwd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return  await _dataAccessProvider.Login(id, pwd);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(Login), ex);
            }
        }

        [HttpPost]
        [Route("updatepwd")]
        public async Task<ActionResult<bool>> ChangePassword(string id, string oldpwd,string newpwd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await _dataAccessProvider.ChangePassword(id, oldpwd, newpwd);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(ChangePassword), ex);
            }
        }
    }
}
