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
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;
using AutoMapper;
using System;
using System.Collections;
using OnyxScoutApplication.Server.Data.Profiles;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Services;
using OnyxScoutApplication.Server.Data.Extensions;
using IdentityServer4.Extensions;
using Newtonsoft.Json;
using OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine($"Configuring services in {env.EnvironmentName} mode ({env.IsDevelopment()})");
            var environmentVariables = Environment.GetEnvironmentVariables();
            
            var connectionString = GetConnectionString();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddIdentityServer(
            //     options =>
            // {
            //     if (!env.IsDevelopment())
            //     {
            //       //  options.PublicOrigin = configuration.GetValue<string>("PublicOrigin");
            //     }
            // }
                ).AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
            {
                options.IdentityResources["openid"].UserClaims.Add("name");
                options.ApiResources.Single().UserClaims.Add("name");
                options.IdentityResources["openid"].UserClaims.Add("role");
                options.ApiResources.Single().UserClaims.Add("role");
            });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

            services.AddAuthentication()
                .AddIdentityServerJwt();
            
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365 * 2);
            });
            
            

            services.Configure<IdentityOptions>(options =>
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

            services.AddControllersWithViews().AddNewtonsoftJson( settings => settings.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddRazorPages();

            services.AddScoped<IScoutFormFormatRepository, ScoutFormFormatRepository>();
            services.AddScoped<IScoutFormFormatUnitOfWork, ScoutFormFormatUnitOfWork>();
            
            services.AddScoped<IScoutFormRepository, ScoutFormRepository>();
            services.AddScoped<IScoutFormUnitOfWork, ScoutFormUnitOfWork>();
            
            services.AddScoped<ICustomEventRepository, CustomEventRepository>();
            services.AddScoped<ICustomEventUnitOfWork, CustomEventUnitOfWork>();

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IApplicationUserUnitOfWork, ApplicationUserUnitOfWork>();

            services.AddTransient(_ => 
                new AmazonS3Client(Environment.GetEnvironmentVariables()["CLOUD_CUBE-ACCESS_KEY_ID"]!.ToString(), 
                    Environment.GetEnvironmentVariables()["CLOUD_CUBE-SECRET_ACCESS_KEY"]!.ToString(),
                    new AmazonS3Config
                    {
                        RegionEndpoint = RegionEndpoint.EUWest1,
                    }));

            services.AddAutoMapper(typeof(ScoutFormProfile));
            services.AddSingleton<ITheBlueAllianceService>(
                    new TheBlueAllianceService(environmentVariables["TBA-KEY"]!.ToString()));
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.Use((ctx, next) =>
                {
                    ctx.Request.Scheme = "https";
                    ctx.SetIdentityServerOrigin(Environment.GetEnvironmentVariables()["PublicOrigin"]!.ToString());
                    return next();
                });
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
            CreateUserRoles(serviceProvider).Wait();
        }
        
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //IdentityResult roleResult;
            //Adding Admin Role
            
            var roles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
            for (int i = 0; i < roles.Count(); i++)
            {
                var role = roles[i];
                var roleCheck = await roleManager.RoleExistsAsync(role.ToString());
                if (!roleCheck)
                {
                    //create the roles and seed them to the database
                    //roleResult =
                        await roleManager.CreateAsync(new ApplicationRole
                    {
                        Id = i.ToString(), Name = role.ToString(), NormalizedName = role.ToString()
                    });
                }
            }
          
            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            //ApplicationUser user = await userManager.FindByEmailAsync("v-nany@hotmail.com");
            //await userManager.AddToRoleAsync(user, "Admin");
        }
        
        private string GetConnectionString()
        {
            var connectionString = "";
            // if (env.IsDevelopment())
            // {
            //     connectionString = configuration.GetConnectionString("DefaultConnection");
            //     return connectionString;
            // }

            connectionString = Environment.GetEnvironmentVariables()["ConnectionString"]!.ToString();
            return connectionString;
        }

    }
}
