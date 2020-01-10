using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Service.Interface
{
    public interface IUserService
    {
        System.Threading.Tasks.Task<IEnumerable<User>> GetUserRecords();
        Task AddUserRecord(User student);
        Task<User> GetUsersingleRecord(string id);
        Task UpdateUserRecord(int id, JsonPatchDocument<User> studentPatch);
        Task DeleteUserRecord(string studentId);
    }
}
