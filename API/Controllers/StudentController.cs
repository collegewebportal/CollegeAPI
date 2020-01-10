using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Helpers;
using Service.Implementation;
using Service.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentService _dataAccessProvider;
        private readonly ILogger _logger;
        public StudentsController(ILogger<StudentsController> logger, IStudentService dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _dataAccessProvider.GetStudentRecords();
        }
        [HttpPost]
        [Route("Create")]
        public async Task Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                await _dataAccessProvider.AddStudentRecord(student);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<Student> Get(string id)
        {
            return await _dataAccessProvider.GetStudentSingleRecord(id);
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task Update(int id,[FromBody] JsonPatchDocument<Student> studentPatch)
        {
            if (ModelState.IsValid)
            {
                await _dataAccessProvider.UpdateStudentRecord(id,studentPatch);
            }
        }
        [HttpDelete]
        [Route("{studentId}")]
        public async Task Delete(string studentId)
        {
            await _dataAccessProvider.DeleteStudentRecord(studentId);
        }
    }
}

