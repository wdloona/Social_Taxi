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

namespace Social_Taxi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly TaxiDbContext _dbContext;

        public ResponseController(TaxiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Откликнуться на заявку пассажира или водителя и изменить состояние заявки
        /// </summary>
        /// <param name="response"> </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> CreateResponseDriver([FromBody] ResponseDriverModel response)
        {
            try
            {
                var ride = await _dbContext.Set<Ride>().Where(z => z.RideId == response.RideId).Select(x =>
                     new RideModel
                     {
                         RideId = x.RideId,
                         CreatorUserId = x.CreatorUserId,
                         FlActive=x.FlActive
                     }).ToListAsync();

                if (ride.FirstOrDefault().FlActive == false)
                {
                    var ResponseToAdd = new Response
                    {
                        ResponseUserId = response.ResponseUserId,
                        RideId = response.RideId,
                        Verification=true
                    };
                    Ride EditRide = await _dbContext.Set<Ride>().Where(z => z.RideId == response.RideId).FirstOrDefaultAsync();
                    EditRide.FlActive = true;
                    await _dbContext.SaveChangesAsync();
                    await _dbContext.AddAsync(ResponseToAdd);
                    await _dbContext.SaveChangesAsync();
                    return 1; 
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// получить все ответы на заявки водителя
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ResponseDriverModel>> GetRedponses([FromQuery] int driverId)
        {
            List<ResponseDriverModel> Rides = new();
            try
            {
                if (driverId != 0)
                {
                    Rides = await _dbContext.Set<Response>().Include(o => o.Ride).Where(z => z.ResponseUserId == driverId).Select(x =>
                        new ResponseDriverModel
                        {
                           ResponseDriverId=x.ResponseDriverId,
                           RideId=x.RideId,
                           ResponseUserId=x.ResponseUserId,
                           RideModel=new RideModel
                           {
                               RideId=x.RideId,
                               BeginDate=x.Ride.BeginDate,
                               FlActive=x.Ride.FlActive,
                               EndDate=x.Ride.EndDate,
                               FlFinished=x.Ride.FlFinished,
                               Address= new AddressModel
                               {
                                   StartCity=x.Ride.StartCity,
                                   EndCity=x.Ride.EndCity
                               }                             
                           }  

                        }).ToListAsync();
                }

            }
            catch
            {

            }

            return Rides;
        }
        /// <summary>
        /// удалить водителя из заявки
        /// </summary>
        /// <param name="responseDriveId"> id брони заявки водителем</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<int> DeleteRide([FromQuery] int responseDriveId)
        {
            try
            {
                Response DeleteResponse = await _dbContext.Set<Response>().Where(z => z.ResponseDriverId == responseDriveId).FirstOrDefaultAsync();
                _dbContext.Remove(DeleteResponse);
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
