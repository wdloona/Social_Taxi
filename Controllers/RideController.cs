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
    public class RideController : ControllerBase
    {
        private readonly TaxiDbContext _dbContext;

        public RideController(TaxiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("rides")]
        public async Task<List<RideModel>> GetRides([FromQuery] int userId)
        {
            List <RideModel> Rides = new();
            try
            {
                if (userId != 0)
                {
                    Rides = await _dbContext.Set<Ride>().Include(o => o.Response).ThenInclude(o => o.User).Where(z => z.CreatorUserId == userId).Select(x =>
                    new RideModel
                    {
                        RideId = x.RideId,
                        CreatorUserId = x.CreatorUserId,
                        RideSearchParams= new RideSearchParamsModel
                        {
                            StartPoint = x.StartPoint,
                            StartCity = x.StartCity,
                            StartStreet = x.StartStreet,
                            StartHome = x.StartHome,
                            EndPoint = x.StartPoint,
                            EndCity = x.EndCity,
                            EndStreet = x.EndStreet,
                            EndHome = x.EndHome,
                            BeginDate = x.BeginDate,
                            EndDate = x.EndDate
                        },
                        SeatsCount = x.SeatsCount,                       
                        FlActive = x.FlActive,
                        FlFinished = x.FlFinished,
                        Description=x.Description,
                        Driver = new UserModel
                        {
                            UserId = (x.Response == null) ? 0 : x.Response.ResponseUserId,
                            UserName = (x.Response == null) ? null : x.Response.User.UserName,
                            UserSurname = (x.Response == null) ? null : x.Response.User.UserSurname,
                            UserPatronymic = (x.Response == null) ? null : x.Response.User.UserPatronymic,
                            Age = (x.Response == null) ? null : x.Response.User.Age,
                            DriverLicenceNumber = (x.Response == null) ? null : x.Response.User.DriverLicenceNumber,
                        }
                    }).ToListAsync();
                }
            }
            catch
            {
            
            }

            return Rides;
        }
        ///// <summary>
        ///// вывести водителя заявки(поездки)
        ///// </summary>
        ///// <param name="rideId"></param>
        ///// <returns></returns>
        //[HttpGet("GetResponseDriverByRideId")]
        //public async Task<List<UserModel>> GetResponseDriverByRideId([FromQuery] int rideId)
        //{
        //    List<UserModel> userModel = new();
            
        //        if (rideId != 0)
        //        {
        //            userModel = await _dbContext.Set<Response>().Include(o => o.User).Where(z => z.RideId == rideId).Select(user =>
        //               new UserModel
        //               {
        //                   UserId=user.ResponseUserId,
        //                   UserName=user.User.UserName,                          
        //                   UserSurname = user.User.UserSurname,
        //                   UserPatronymic = user.User.UserPatronymic,
        //                   Age = user.User.Age,
        //                   DriverLicenceNumber = user.User.DriverLicenceNumber,
        //                   DrivingExperience = user.User.DrivingExperience,

        //        }).ToListAsync();
        //        }        
           
        //    return userModel;
        //}

        [HttpGet]
        public async Task<RideModel> GetRide([FromQuery] int rideId)
        {
            RideModel Ride = new();
            try
            {
                if (rideId != 0)
                {
                    Ride = await _dbContext.Set<Ride>().Include(o=>o.Response).ThenInclude(o=>o.User).Where(z => z.RideId == rideId).Select(x =>
                    new RideModel
                    {
                        RideId = x.RideId,
                        CreatorUserId = x.CreatorUserId,
                        RideSearchParams = new RideSearchParamsModel
                        {
                            StartPoint = x.StartPoint,
                            StartCity = x.StartCity,
                            StartStreet = x.StartStreet,
                            StartHome = x.StartHome,
                            EndPoint = x.StartPoint,
                            EndCity = x.EndCity,
                            EndStreet = x.EndStreet,
                            EndHome = x.EndHome,
                            BeginDate = x.BeginDate,
                            EndDate = x.EndDate
                        },
                        SeatsCount = x.SeatsCount,                      
                        FlActive = x.FlActive,
                        FlFinished = x.FlFinished,
                        Driver= new UserModel
                        {
                            UserId = (x.Response==null)?0:x.Response.ResponseUserId,
                            UserName = (x.Response == null) ? null: x.Response.User.UserName,
                            UserSurname = (x.Response == null) ? null : x.Response.User.UserSurname,
                            UserPatronymic = (x.Response == null) ? null : x.Response.User.UserPatronymic,
                            Age = (x.Response == null) ? null : x.Response.User.Age,
                            DriverLicenceNumber= (x.Response == null) ? null : x.Response.User.DriverLicenceNumber,

                        }
                        
                    }).FirstOrDefaultAsync();
                }

            }
            catch
            {

            }

            return Ride;
        }


        [HttpPost("byaddress")]
        public async Task<List<RideModel>> GetRidesByAddress([FromBody] RideSearchParamsModel address, [FromQuery] bool flIsDriver)
        {
            List<RideModel> Rides = new();
            try
            {
                IQueryable<Ride> query = _dbContext.Set<Ride>().Where(z => (z.StartCity == address.StartCity || string.IsNullOrEmpty(address.StartCity) == true) &&
                                                                       (z.StartStreet == address.StartStreet || string.IsNullOrEmpty(address.StartStreet) == true) &&
                                                                        (z.StartHome == address.StartHome || string.IsNullOrEmpty(address.StartHome) == true) &&
                                                                          (z.EndCity == address.EndCity || string.IsNullOrEmpty(address.EndCity) == true) &&
                                                                            (z.EndStreet == address.EndStreet || string.IsNullOrEmpty(address.EndStreet) == true) &&
                                                                              (z.EndHome == address.EndHome || string.IsNullOrEmpty(address.EndHome) == true) && z.FlFinished==false && z.FlActive==true);


                //подходит ли адрес
                Rides = await query.Select(x =>
                    new RideModel
                    {
                        RideId = x.RideId,
                        CreatorUserId = x.CreatorUserId,
                        RideSearchParams= new RideSearchParamsModel 
                        {
                            StartPoint = x.StartPoint,
                            StartCity = x.StartCity,
                            StartStreet = x.StartStreet,
                            StartHome = x.StartHome,
                            EndPoint = x.StartPoint,
                            EndCity = x.EndCity,
                            EndStreet = x.EndStreet,
                            EndHome = x.EndHome,
                            BeginDate = x.BeginDate,
                            EndDate = x.EndDate,
                        },
                        SeatsCount = x.SeatsCount,                      
                        FlActive = x.FlActive
                    }).ToListAsync();

            }
            catch(Exception ex)
            {

            }

            return Rides;
        }

        [HttpPut]
        public async Task<int> EditRide([FromBody] RideModel ride)
        {
            try
            {
                Ride EditRide =  await _dbContext.Set<Ride>().Where(z => z.RideId == ride.RideId).FirstOrDefaultAsync();
                    EditRide.StartPoint = ride.RideSearchParams.StartPoint;
                    EditRide.StartCity = ride.RideSearchParams.StartCity;
                    EditRide.StartStreet = ride.RideSearchParams.StartStreet;
                    EditRide.StartHome = ride.RideSearchParams.StartHome;
                    EditRide.EndPoint = ride.RideSearchParams.EndPoint;
                    EditRide.EndCity = ride.RideSearchParams.EndCity;
                    EditRide.EndStreet = ride.RideSearchParams.EndStreet;
                    EditRide.EndHome = ride.RideSearchParams.EndHome;
                    EditRide.SeatsCount = ride.SeatsCount;
                    EditRide.BeginDate = ride.RideSearchParams.BeginDate;
                    EditRide.EndDate = ride.RideSearchParams.EndDate;
                    EditRide.Description = ride.Description;
                    
                    await _dbContext.SaveChangesAsync();
                    return 1;           
            }
            catch
            {
                return 0;
            }
        }

        [HttpPost]
        public async Task<int> CreateRide([FromBody] RideModel ride)
        {
            try
            {
                var RideToAdd = new Ride
                {
                    CreatorUserId = ride.CreatorUserId,
                    StartPoint = ride.RideSearchParams.StartPoint,
                    StartCity = ride.RideSearchParams.StartCity,
                    StartStreet = ride.RideSearchParams.StartStreet,
                    StartHome = ride.RideSearchParams.StartHome,
                    EndPoint = ride.RideSearchParams.StartPoint,
                    EndCity = ride.RideSearchParams.EndCity,
                    EndStreet = ride.RideSearchParams.EndStreet,
                    EndHome = ride.RideSearchParams.EndHome,
                    SeatsCount = ride.SeatsCount,
                    BeginDate = ride.RideSearchParams.BeginDate,
                    EndDate = ride.RideSearchParams.EndDate,
                    FlActive = true,
                    FlFinished = false,
                    Description=ride.Description
                    
                };
                await _dbContext.AddAsync(RideToAdd);
                await _dbContext.SaveChangesAsync();

                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        [HttpDelete]
        public async Task<int> DeleteRide([FromQuery] int rideId)
        {
            try
            {
                Ride EditRide = await _dbContext.Set<Ride>().Where(z => z.RideId == rideId).FirstOrDefaultAsync();
                _dbContext.Remove(EditRide);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
