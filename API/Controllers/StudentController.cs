using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentService _dataAccessProvider;
        public StudentsController(IStudentService dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _dataAccessProvider.GetStudentRecords();
        }
        [HttpPost]
        [Route("Create")]
        public async Task Create(Student student)
        {
            if (ModelState.IsValid)
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
        [HttpPut]
        [Route("Edit")]
        public async Task Update([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                await _dataAccessProvider.UpdateStudentRecord(student);
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

