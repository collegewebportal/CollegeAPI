using Domain;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
   public interface IStudentService
    {
        Task AddStudentRecord(Student student);
        Task UpdateStudentRecord(int id,JsonPatchDocument<Student>  studentPatch );
        Task DeleteStudentRecord(string studentId);
        Task<Student> GetStudentSingleRecord(string studentId);
        Task<IEnumerable<Student>> GetStudentRecords();
    }
}
