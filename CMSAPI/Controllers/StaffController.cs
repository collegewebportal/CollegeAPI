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
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _dataAccessProvider;
        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger, IStaffService dataAccessProvider)
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
        public async Task<ActionResult> Create(Staff staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Registering Staff named {0}", staff.FirstName);
                    await _dataAccessProvider.AddStaffRecord(staff);
                    _logger.LogInformation("Staff named {0} registered succesfully", staff.FirstName);
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
        public async Task<ActionResult<Staff>> Get(int? id)
        {
            try
            {
                return await _dataAccessProvider.GetStaffsingleRecordAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(Get), ex);
            }
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] JsonPatchDocument<Staff> StaffPatch)
        {
            try
            {
                if (ModelState.IsValid)
                    return await _dataAccessProvider.UpdateStaffRecord(id, StaffPatch);
                else
                    return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(Update), ex);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<int>> Delete(int? StaffId)
        {
            try
            {
                return await _dataAccessProvider.DeleteStaffRecordAsync(StaffId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(Delete), ex);
            }
        }

    }
}
