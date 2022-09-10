using Social_Taxi.Servicies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Social_Taxi.Authorization;
using Social_Taxi.EntityFramework;
using Social_Taxi.Models;
using System.Collections.Generic;
using Social_Taxi.EntityFramework.Tables;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Social_Taxi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TaxiDbContext _dbContext;

        public UserController(TaxiDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPut]
        public async Task<int> EditUser([FromBody] UserModel user)
        {
            var currentUser = AuthorizationHelper.GetUser(User);
            try
            {
                User User = await _dbContext.Set<User>().Where(z => z.UserId == user.UserId).FirstOrDefaultAsync();
                if (User != null)
                {
                    User.UserName = user.UserName;
                    User.UserSurname = user.UserSurname;
                    User.UserPatronymic = user.UserPatronymic;
                    User.Age = user.Age;
                    User.DriverLicenceNumber = user.DriverLicenceNumber;
                    User.DrivingExperience = user.DrivingExperience;

                }
                if (currentUser.RoleId == 1 || user.UserId == currentUser.UserId)
                {
                    await _dbContext.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        [HttpPost()]
        public async Task<int> AddUser([FromBody] UserModel user)
        {
            var currentUser = AuthorizationHelper.GetUser(User);
            try
            {
                if (currentUser.RoleId == 1)
                {
                    User AddUser = new();
                    AddUser.RoleId = user.RoleId;
                    AddUser.Login = user.Login;
                    AddUser.Password = Authorization.AuthorizationOptions.ComputePassSha256Hash(user.Password);
                    AddUser.UserName = user.UserName;
                    AddUser.UserSurname = user.UserSurname;
                    AddUser.UserPatronymic = user.UserPatronymic;
                    AddUser.Age = user.Age;
                    AddUser.PhoneNumber = user.PhoneNumber;
                    AddUser.DriverLicenceNumber = user.DriverLicenceNumber;
                    AddUser.DrivingExperience = user.DrivingExperience;

                
                    await _dbContext.AddAsync(AddUser);
                    await _dbContext.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
                
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
    }
}
