using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _dataAccessProvider;
        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger,IStaffService dataAccessProvider)
        {
            _logger = logger;
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAll()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Getting All Staffs.");
                    var response = await _dataAccessProvider.GetStaffRecords();
                    _logger.LogInformation("Operation Completed Succesfully.");
                    return CreatedAtAction(nameof(GetAll), response);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(GetAll), ex);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create(Staff Staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Registering Staff named {0}", Staff.FirstName);
                    await _dataAccessProvider.AddStaffRecord(Staff);
                    _logger.LogInformation("Staff named {0} registered succesfully", Staff.FirstName);
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(Create), ex);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<Staff> Get(int? id)
        {
            return await _dataAccessProvider.GetStaffsingleRecordAsync(id);
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task Update(int id, [FromBody] JsonPatchDocument<Staff> StaffPatch)
        {
            if (ModelState.IsValid)
            {
                await _dataAccessProvider.UpdateStaffRecord(id, StaffPatch);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int? StaffId)
        {
            await _dataAccessProvider.DeleteStaffRecordAsync(StaffId);
        }

    }
}
