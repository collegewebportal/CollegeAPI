using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class StaffService : IStaffService
    {
        private CMSDBContext _context { get; set; }
        public StaffService(CMSDBContext context)
        {
            _context = context;
        }
        public Task AddStaffRecord(Staff Staff)
        {
            try
            {
                Staff.Password = CreateRandomPassword(8);
                _context.Staffs.Add(Staff);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteStaffRecordAsync(int? staffId)
        {
            int result = 0;
            if (_context != null)
            {
                //Find the post for specific post id
                var staff = await _context.Staffs.FirstOrDefaultAsync(x => x.Id == staffId);

                if (staff != null)
                {
                    //Delete that post
                    _context.Staffs.Remove(staff);

                    //Commit the transaction
                    result = await _context.SaveChangesAsync();
                }
            }
            return result;
        }

        public Task<List<Staff>> GetStaffRecords()
        {
            return _context.Staffs.ToListAsync();
        }

        public async Task<Staff> GetStaffsingleRecordAsync(int? id)
        {
            return await _context.Staffs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateStaffRecord(int id, JsonPatchDocument<Staff> staffPatch)
        {
            Staff staffToUpdate = await _context.Staffs.FirstOrDefaultAsync(x => x.Id == id );
            _context.Staffs.Update(staffPatch);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Login(string id, string pwd)
        {
            bool userFound = false;
            var staff = await _context.Staffs.FirstOrDefaultAsync(x => x.Email == id && x.Password == pwd);

            if (staff != null)
                userFound = true;

            return userFound;
        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public async Task<bool> ChangePassword(string id, string oldpwd, string newpwd)
        {
            try
            {
                Staff staffToUpdate= await _context.Staffs.FirstOrDefaultAsync(x => x.Email == id && x.Password == oldpwd);
                staffToUpdate.Password = newpwd;
                _context.Staffs.Update(staffToUpdate);
                await _context.SaveChangesAsync();
                return true;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
