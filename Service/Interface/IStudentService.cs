using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
   public interface IStudentService
    {
        Task AddStudentRecord(Student student);
        Task UpdateStudentRecord(Student student);
        Task DeleteStudentRecord(string studentId);
        Task<Student> GetStudentSingleRecord(string studentId);
        Task<IEnumerable<Student>> GetStudentRecords();
    }
}
