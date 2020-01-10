using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Helpers;
using Service.Implementation;
using Service.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _dataAccessProvider;
        private readonly ILogger _logger;
        public UserController(ILogger<UserController> logger, IUserService dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dataAccessProvider.GetUserRecords();
        }
        [HttpPost]
        [Route("Create")]
        public async Task Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _dataAccessProvider.AddUserRecord(user);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<User> Get(string id)
        {
            return await _dataAccessProvider.GetUsersingleRecord(id);
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task Update(int id,[FromBody] JsonPatchDocument<User> userPatch)
        {
            if (ModelState.IsValid)
            {
                await _dataAccessProvider.UpdateUserRecord(id,userPatch);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(string userId)
        {
            await _dataAccessProvider.DeleteUserRecord(userId);
        }
    }
}

