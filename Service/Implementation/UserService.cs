using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Persistance;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class UserService : IUserService
    {
        private CMSDBContext _context { get; set; }
        public UserService(CMSDBContext context)
        {
            _context = context;
        }
        public Task AddUserRecord(User user)
        {
            try
            {
                _context.User.Add(user);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task DeleteUserRecord(string studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUserRecords()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUsersingleRecord(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserRecord(int id, JsonPatchDocument<User> studentPatch)
        {
            throw new NotImplementedException();
        }
    }
}
