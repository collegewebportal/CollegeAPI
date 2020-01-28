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
    public class SourceService : ISourceService
    {
        private CMSDBContext _context { get; set; }
        public SourceService(CMSDBContext context)
        {
            _context = context;
        }
        public Task AddSourceRecord(Source Source)
        {
            try
            {
                _context.Sources.Add(Source);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteSourceRecordAsync(int? SourceId)
        {
            int result = 0;
            if (_context != null)
            {
                //Find the post for specific post id
                var Source = await _context.Sources.FirstOrDefaultAsync(x => x.Id == SourceId);

                if (Source != null)
                {
                    //Delete that post
                    _context.Sources.Remove(Source);

                    //Commit the transaction
                    result = await _context.SaveChangesAsync();
                }
            }
            return result;
        }

        public Task<List<Source>> GetSourceRecords()
        {
            return _context.Sources.ToListAsync();
        }

        public async Task<Source> GetSourcesingleRecordAsync(int? id)
        {
            return await _context.Sources.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateSourceRecord(int id, JsonPatchDocument<Source> SourcePatch)
        {
            Source SourceToUpdate = await _context.Sources.FirstOrDefaultAsync(x => x.Id == id );
            _context.Sources.Update(SourcePatch);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
