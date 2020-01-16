using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
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
                //Staff.Password=
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

        public Task UpdateStaffRecord(int id, JsonPatchDocument<Staff> staffPatch)
        {
            throw new NotImplementedException();
        }
    }
}
