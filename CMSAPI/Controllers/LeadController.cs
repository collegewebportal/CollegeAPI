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
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _dataAccessProvider;
        private readonly ILogger<LeadController> _logger;

        public LeadController(ILogger<LeadController> logger, ILeadService dataAccessProvider)
        {
            _logger = logger;
            _dataAccessProvider = dataAccessProvider;
        }

        public async Task<ActionResult<IEnumerable<Lead>>> GetAll()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Getting All Leads.");
                    var response = await _dataAccessProvider.GetLeadRecords();
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
        public async Task<ActionResult> Create(Lead Lead)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Registering Lead named {0}", Lead.FirstName);
                    await _dataAccessProvider.AddLeadRecord(Lead);
                    _logger.LogInformation("Lead named {0} registered succesfully", Lead.FirstName);
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
        public async Task<ActionResult<Lead>> Get(int? id)
        {
            try
            {
                return await _dataAccessProvider.GetLeadsingleRecordAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(Get), ex);
            }
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] JsonPatchDocument<Lead> LeadPatch)
        {
            try
            {
                if (ModelState.IsValid)
                    return await _dataAccessProvider.UpdateLeadRecord(id, LeadPatch);
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
        public async Task<ActionResult<int>> Delete(int? LeadId)
        {
            try
            {
                return await _dataAccessProvider.DeleteLeadRecordAsync(LeadId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(Delete), ex);
            }
        }
    }
}
