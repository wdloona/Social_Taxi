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
            try
            {
                var currentUser = AuthorizationHelper.GetUser(User);
                if (currentUser.RoleId == 1 || user.UserId == currentUser.UserId)
                {
                    User User = await _dbContext.Set<User>().Where(z => z.UserId == user.UserId).FirstOrDefaultAsync();
                    if (User != null)
                    {
                        User.UserName = user.UserName;
                        User.UserSurname = user.UserSurname;
                        User.UserPatronymic = user.UserPatronymic;
                        User.PhoneNumber = user.PhoneNumber;
                        User.Password = Authorization.AuthorizationOptions.ComputePassSha256Hash(user.Password);
                        if (currentUser.RoleId == 1)
                        {
                            User.Age = user.Age;
                            User.DriverLicenceNumber = user.DriverLicenceNumber;
                            User.DrivingExperience = user.DrivingExperience;
                            User.IsBlocked = user.IsBlocked;
                        }
                    }             
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
                    User AddUser = new()
                    { 
                        RoleId = user.RoleId,
                        Login = user.Login,
                        Password = Authorization.AuthorizationOptions.ComputePassSha256Hash(user.Password),
                        UserName = user.UserName,
                        UserSurname = user.UserSurname,
                        UserPatronymic = user.UserPatronymic,
                        Age = user.Age,
                        PhoneNumber = user.PhoneNumber,
                        DriverLicenceNumber = user.DriverLicenceNumber,
                        DrivingExperience = user.DrivingExperience,
                        IsBlocked=user.IsBlocked
                    };

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
