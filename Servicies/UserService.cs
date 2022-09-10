using Social_Taxi.Authorization;
using Social_Taxi.EntityFramework;
using Social_Taxi.EntityFramework.Tables;
using Social_Taxi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Taxi.Servicies
{
    public class UserService
    {
        private readonly TaxiDbContext _taxiDbContext;

        public UserService(TaxiDbContext taxiDbContext)
        {
            _taxiDbContext = taxiDbContext;
        }

        public async Task<User> GetUser(LoginModel loginModel)
        {
            var hashedPass = AuthorizationOptions.ComputePassSha256Hash(loginModel.Password);
            var user = await _taxiDbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Login == loginModel.Login && u.Password == hashedPass);

            return user;
        }

    }
}
