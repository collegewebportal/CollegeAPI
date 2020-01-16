﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Service.Interface
{
    public interface IStaffService
    {
        Task<List<Staff>> GetStaffRecords();
        Task AddStaffRecord(Staff staff);
        Task<Staff> GetStaffsingleRecordAsync(int? id);
        Task UpdateStaffRecord(int id, JsonPatchDocument<Staff> staffPatch);
        Task<int> DeleteStaffRecordAsync(int? staffId);
    }
}
