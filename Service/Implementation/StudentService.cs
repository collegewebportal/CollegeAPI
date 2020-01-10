using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Service.Interface;

namespace Service.Implementation
{
    public class StudentService : IStudentService
    {
        private CMSDBContext _context { get; set; }
        public StudentService(CMSDBContext context)
        {
            _context = context;
        }

        public Task AddStudentRecord(Student student)
        {
            _context.Student.Add(student);
            return _context.SaveChangesAsync();
        }

        public Task UpdateStudentRecord(int id, JsonPatchDocument<Student> studentPatch)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteStudentRecord(string studentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Student> GetStudentSingleRecord(string studentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetStudentRecords()
        {
            throw new System.NotImplementedException();
        }
    }


}

