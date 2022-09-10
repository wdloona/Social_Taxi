using CSharp_React.EntityFramework;
using CSharp_React.Hubs;
using CSharp_React.Servicies;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Threading.Tasks;
using WebPush;

namespace CSharp_React
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = 
                    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddSwaggerGen(option =>
            {
                foreach (var path in Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml"))
                {
                    option.IncludeXmlComments(path, true);
                }
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddLogging(builder => builder.AddConsole().AddDebug());

            services.AddAuthentication(options => { options.DefaultScheme = "CookiesAuth"; })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Authorization.AuthorizationOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = Authorization.AuthorizationOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = Authorization.AuthorizationOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };

                }).AddCookie("CookiesAuth", options => {
                    options.Cookie.Name = "AccessToken";
                    options.Cookie.HttpOnly = false;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder("CookiesAuth").RequireAuthenticatedUser().Build();
            });

            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy.SetIsOriginAllowed(origin => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddScoped<TaxiDbContext>();
            services.AddScoped<UserService>();
            services.AddScoped<RightService>(); 
            services.AddScoped<TryService>();

            var vapidDetails = new VapidDetails(
                Configuration.GetValue<string>("VapidDetails:Subject"),
                Configuration.GetValue<string>("VapidDetails:PublicKey"),
                Configuration.GetValue<string>("VapidDetails:PrivateKey"));
            services.AddTransient(c => vapidDetails);
                        
            services.AddScoped<NotificationSubscribeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseCors("ClientPermission");

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/hubs/chat");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
