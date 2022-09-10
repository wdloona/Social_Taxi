using CSharp_React.Servicies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CSharp_React.Authorization;
using CSharp_React.EntityFramework;
using CSharp_React.Models;
using System.Collections.Generic;
using CSharp_React.EntityFramework.Tables;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace CSharp_React.Controllers
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
                        Address= new AddressModel
                        {
                            StartPoint = x.StartPoint,
                            StartCity = x.StartCity,
                            StartStreet = x.StartStreet,
                            StartHome = x.StartHome,
                            EndPoint = x.StartPoint,
                            EndCity = x.EndCity,
                            EndStreet = x.EndStreet,
                            EndHome = x.EndHome,
                        },
                        SeatsCount = x.SeatsCount,
                        BeginDate = x.BeginDate,
                        EndDate = x.EndDate,
                        FlActive = x.FlActive,
                        FlFinished = x.FlFinished,
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
                        Address = new AddressModel
                        {
                            StartPoint = x.StartPoint,
                            StartCity = x.StartCity,
                            StartStreet = x.StartStreet,
                            StartHome = x.StartHome,
                            EndPoint = x.StartPoint,
                            EndCity = x.EndCity,
                            EndStreet = x.EndStreet,
                            EndHome = x.EndHome,
                        },
                        SeatsCount = x.SeatsCount,
                        BeginDate = x.BeginDate,
                        EndDate = x.EndDate,
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
        public async Task<List<RideModel>> GetRidesByAddress([FromBody] AddressModel address)
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
                        Address= new AddressModel 
                        {
                            StartPoint = x.StartPoint,
                            StartCity = x.StartCity,
                            StartStreet = x.StartStreet,
                            StartHome = x.StartHome,
                            EndPoint = x.StartPoint,
                            EndCity = x.EndCity,
                            EndStreet = x.EndStreet,
                            EndHome = x.EndHome,
                        },
                        SeatsCount = x.SeatsCount,
                        BeginDate = x.BeginDate,
                        EndDate = x.EndDate,
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
                    EditRide.StartPoint = ride.Address.StartPoint;
                    EditRide.StartCity = ride.Address.StartCity;
                    EditRide.StartStreet = ride.Address.StartStreet;
                    EditRide.StartHome = ride.Address.StartHome;
                    EditRide.EndPoint = ride.Address.EndPoint;
                    EditRide.EndCity = ride.Address.EndCity;
                    EditRide.EndStreet = ride.Address.EndStreet;
                    EditRide.EndHome = ride.Address.EndHome;
                    EditRide.SeatsCount = ride.SeatsCount;
                    EditRide.BeginDate = ride.BeginDate;
                    EditRide.EndDate = ride.EndDate;
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
                    StartPoint = ride.Address.StartPoint,
                    StartCity = ride.Address.StartCity,
                    StartStreet = ride.Address.StartStreet,
                    StartHome = ride.Address.StartHome,
                    EndPoint = ride.Address.StartPoint,
                    EndCity = ride.Address.EndCity,
                    EndStreet = ride.Address.EndStreet,
                    EndHome = ride.Address.EndHome,
                    SeatsCount = ride.SeatsCount,
                    BeginDate = ride.BeginDate,
                    EndDate = ride.EndDate,
                    FlActive = true,
                    FlFinished = false
                    
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
