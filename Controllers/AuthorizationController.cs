using Social_Taxi.Authorization;
using Social_Taxi.EntityFramework.Tables;
using Social_Taxi.Models;
using Social_Taxi.Servicies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Social_Taxi.Controllers
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
        public BaseModel<string> GetTokenFromCookie()
        {
            var hasToken = HttpContext.Request.Cookies.ContainsKey("AccessToken");

            return new BaseModel<string>(value: hasToken ? HttpContext.Request.Cookies["AccessToken"] : null);
        }

        [HttpPost]
        public async Task<BaseModel<int>> AutorizeUser([FromBody] LoginModel loginModel)
        {
            var userId = 0;
            if (!User.Identity.IsAuthenticated)
            {
                var blockDateTime = _tryService.CheckLoginTry(new TryModel { 
                    IP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    TryDateTime = DateTime.Now
                });

                if (blockDateTime.HasValue)
                    return new BaseModel<int>($"Вход заблокирован до {blockDateTime.Value}!");

                var user = await _userService.GetUser(loginModel);
                var identity = GetIdentity(user);
                if (identity == null)
                {
                    return new BaseModel<int>("Не верный логин или пароль!");
                }
                userId = user.UserId;
                HttpContext.Response.Cookies.Append("UserId", userId.ToString());

                await HttpContext.SignInAsync("CookiesAuth", new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1) //До конца текущего дня
                });
            }

            return new BaseModel<int>(userId);
        }

        [HttpGet("Logout")]
        public async Task LogOutUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync("CookiesAuth");
            }
        }

        [Authorize]
        [HttpGet("user")]
        public BaseModel<UserModel> GetUserInfo()
        {
            return new BaseModel<UserModel>(AuthorizationHelper.GetUser(User));
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
