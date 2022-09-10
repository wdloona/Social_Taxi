using CSharp_React.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace CSharp_React.Authorization
{
    public static class AuthorizationHelper
    {
        public static T GetValue<T>(this ClaimsPrincipal claims, string name)
        {
            try
            {
                var val = claims.FindFirst(o => o.Type == name).Value;
                return (T)Convert.ChangeType(val, typeof(T));
            }
            catch
            {
                return default;
            }
        }

        public static UserModel GetUser(ClaimsPrincipal claims)
        {
            var User = new UserModel
            {
                UserId = int.Parse(claims.Claims.First(o => o.Type == ClaimTypes.NameIdentifier).Value),
                UserName = claims.GetValue<string>(ClaimsIdentity.DefaultNameClaimType)
            };

            return User;
        }
    }
}
