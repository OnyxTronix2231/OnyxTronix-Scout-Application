using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using OnyxScoutApplication.Server.Data;
using OnyxScoutApplication.Server.Models;
using OnyxScoutApplication.Server.Data.Presistance.Repositories;
using OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork;
using OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork.interfaces;
using AutoMapper;
using System;
using OnyxScoutApplication.Server.Data.Profiles;
using OnyxScoutApplication.Shared.Data.Presistance.TheBlueAlliance;
using OnyxScoutApplication.Server.Data.Presistance.TheBlueAlliance;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Services;
using OnyxScoutApplication.Server.Data.Extensions;

namespace OnyxScoutApplication.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddIdentityServer()
                 .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                 {
                     options.IdentityResources["openid"].UserClaims.Add("name");
                        options.ApiResources.Single().UserClaims.Add("name");
                     options.IdentityResources["openid"].UserClaims.Add("role");
                     options.ApiResources.Single().UserClaims.Add("role");
                 });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.Configure<IdentityOptions>(options =>
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);



            //to validate the token which has been sent by clients

           // services.AddAuthentication().AddOpenIdConnect();
            //    .AddIdentityServerJwt()
            //    .AddOpenIdConnect(option =>
            //    option.Events = new OpenIdConnectEvents
            //    {
            //        OnTokenResponseReceived = xxx =>
            //        {
            //            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //            JwtSecurityToken jwt = handler.ReadJwtToken(xxx.TokenEndpointResponse.AccessToken);

            //            var claimsIdentity = (ClaimsIdentity)xxx.Principal.Identity;
            //            claimsIdentity.AddClaims(jwt.Claims);
            //            return Task.FromResult(0);
            //        }
            //    });
                //.AddIdentityServerJwt()
                //.AddJwtBearer(config =>
                //{
                //    config.RequireHttpsMetadata = false;
                //    config.SaveToken = true;

                //    config.TokenValidationParameters = new TokenValidationParameters
                //    {
                //        ValidateIssuer = true,
                //        ValidateAudience = true,
                //        ValidateLifetime = true,
                //        ValidateIssuerSigningKey = true,
                //        ValidIssuer = Configuration["JwtIssuer"],
                //        ValidAudience = Configuration["JwtAudience"],
                //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
                //    };
                //});


            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IScoutFormFormatRepository, ScoutFormFormatRepository>();
            services.AddScoped<IScoutFormRepository, ScoutFormRepository>();

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IScoutFormFormatUnitOfWork, ScoutFormFormatUnitOfWork>();
            services.AddScoped<IScoutFormUnitOfWork, ScoutFormUnitOfWork>();
            services.AddScoped<IApplicationUserUnitOfWork, ApplicationUserUnitOfWork>();

            services.AddAutoMapper(typeof(ScoutFormProfile));
            services.AddSingleton<ITheBlueAllianceService>(new TheBlueAllianceService("bX9cxVNbMq3WzxTDiWjfblxrk58HZj65QyToW1hvXURrtjHtuuXsFujFC5j6iPus"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
