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
    public class SourceController : ControllerBase
    {
        private readonly ISourceService _dataAccessProvider;
        private readonly ILogger<SourceController> _logger;
        public SourceController(ILogger<SourceController> logger, ISourceService dataAccessProvider)
        {
            _logger = logger;
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Source>>> GetAll()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Getting All Sources.");
                    var response = await _dataAccessProvider.GetSourceRecords();
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
        public async Task<ActionResult> Create(Source Source)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Registering Source named {0}", Source.Name);
                    await _dataAccessProvider.AddSourceRecord(Source);
                    _logger.LogInformation("Source named {0} registered succesfully", Source.Name);
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
        public async Task<ActionResult<Source>> Get(int? id)
        {
            try
            {
                return await _dataAccessProvider.GetSourcesingleRecordAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(Get), ex);
            }
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] JsonPatchDocument<Source> SourcePatch)
        {
            try
            {
                if (ModelState.IsValid)
                    return await _dataAccessProvider.UpdateSourceRecord(id, SourcePatch);
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
        public async Task<ActionResult<int>> Delete(int? SourceId)
        {
            try
            {
                return await _dataAccessProvider.DeleteSourceRecordAsync(SourceId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return CreatedAtAction(nameof(Delete), ex);
            }
        }
    }
}
