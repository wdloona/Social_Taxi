using CSharp_React.Authorization;
using CSharp_React.EntityFramework.Tables;
using CSharp_React.Models;
using CSharp_React.Servicies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CSharp_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TryService _tryService;

        public AuthorizationController(UserService UserService, TryService tryService)
        {
            _userService = UserService;
            _tryService = tryService;
        }

        [HttpGet]
        public string GetTokenFromCookie()
        {
            var hasToken = HttpContext.Request.Cookies.ContainsKey("AccessToken");
            return hasToken ? HttpContext.Request.Cookies["AccessToken"] : null;
        }

        [HttpPost]
        public async Task<LoginResponseModel> AutorizeUser([FromBody] LoginModel loginModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                var blockDateTime = _tryService.CheckLoginTry(new TryModel { 
                    IP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    TryDateTime = DateTime.Now
                });

                if (blockDateTime.HasValue)
                    return new LoginResponseModel()
                    {
                        Success = false,
                        Message = $"Вход заблокирован до {blockDateTime.Value}!"
                    };

                var user = await _userService.GetUser(loginModel);
                var identity = GetIdentity(user);
                if (identity == null)
                {
                    return new LoginResponseModel() 
                    { 
                        Success = false,
                        Message = "Не верный логин или пароль!"
                    };
                }

                HttpContext.Response.Cookies.Append("UserId", user.UserId.ToString());

                await HttpContext.SignInAsync("CookiesAuth", new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1) //До конца текущего дня
                });
            }

            return new LoginResponseModel();
        }

        [HttpGet("Logout")]
        public async Task<string> LogOutUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync("CookiesAuth");
            }

            return null;
        }

        [Authorize]
        [HttpGet("user")]
        public UserModel GetUserInfo()
        {
            return AuthorizationHelper.GetUser(User);
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString(), ClaimValueTypes.Integer32),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString(), ClaimValueTypes.Integer32),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AccessToken", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователь не найден
            return null;
        }
    }
}
