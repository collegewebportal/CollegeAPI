using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Service.Interface
{
    public interface ILeadService
    {
        Task<List<Lead>> GetLeadRecords();
        Task AddLeadRecord(Lead Lead);
        Task<Lead> GetLeadsingleRecordAsync(int? id);
        Task<bool> UpdateLeadRecord(int id, JsonPatchDocument<Lead> LeadPatch);
        Task<int> DeleteLeadRecordAsync(int? LeadId);
    }
}
