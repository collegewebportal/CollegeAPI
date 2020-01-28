using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Service.Interface
{
    public interface ISourceService
    {
        Task<List<Source>> GetSourceRecords();
        Task AddSourceRecord(Source Source);
        Task<Source> GetSourcesingleRecordAsync(int? id);
        Task<bool> UpdateSourceRecord(int id, JsonPatchDocument<Source> SourcePatch);
        Task<int> DeleteSourceRecordAsync(int? SourceId);
    }
}
