using CSharp_React.Authorization;
using CSharp_React.EntityFramework;
using CSharp_React.EntityFramework.Tables;
using CSharp_React.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_React.Servicies
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
