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
    public class LeadService : ILeadService
    {
        private CMSDBContext _context { get; set; }
        public LeadService(CMSDBContext context)
        {
            _context = context;
        }
        public Task AddLeadRecord(Lead Lead)
        {
            try
            {
                _context.Leads.Add(Lead);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteLeadRecordAsync(int? LeadId)
        {
            int result = 0;
            if (_context != null)
            {
                //Find the post for specific post id
                var Lead = await _context.Leads.FirstOrDefaultAsync(x => x.Id == LeadId);

                if (Lead != null)
                {
                    //Delete that post
                    _context.Leads.Remove(Lead);

                    //Commit the transaction
                    result = await _context.SaveChangesAsync();
                }
            }
            return result;
        }

        public Task<List<Lead>> GetLeadRecords()
        {
            return _context.Leads.ToListAsync();
        }

        public async Task<Lead> GetLeadsingleRecordAsync(int? id)
        {
            return await _context.Leads.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateLeadRecord(int id, JsonPatchDocument<Lead> LeadPatch)
        {
            Lead LeadToUpdate = await _context.Leads.FirstOrDefaultAsync(x => x.Id == id );
            _context.Leads.Update(LeadPatch);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
