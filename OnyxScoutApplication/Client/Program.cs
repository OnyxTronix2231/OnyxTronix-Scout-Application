using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using OnyxScoutApplication.Client.Others.Managers;
using FluentValidation;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.FluentValidations;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using OnyxScoutApplication.Client.Others.Extensions;
using OnyxScoutApplication.Client.Others.Objects;
using OnyxScoutApplication.Client.Others.Objects.Analyzers;
using OnyxScoutApplication.Shared.Models.CustomeEventModels;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            ConfigureServices(builder.Services, builder);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services, WebAssemblyHostBuilder builder)
        {
            services.AddHttpClient("OnyxScoutApplication.ServerAPI",
                    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            services.AddTransient(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("OnyxScoutApplication.ServerAPI"));

            builder.Services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<RolesClaimsPrincipalFactory>();


            services.AddSyncfusionBlazor();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                "MzgwMDc5QDMxMzgyZTM0MmUzMFM2RGdsNWJYWDRvR3RQdVdzd1BmOGM0djYxMlNTVWk4RWE0TUxVM3BjVTg9");
            services.AddTransient<HttpClientManager>();
            services.AddSingleton<NotificationManager>();
            services.AddTransient<IValidator<ScoutFormFormatDto>, ScoutFormFormatValidator>();
            services.AddTransient<IValidator<FieldDto>, FieldValidator>();
            services.AddTransient<IValidator<ApplicationUserDto>, ApplicationUserValidator>();
            services.AddTransient<IValidator<ScoutFormDto>, ScoutFormValidator>();
            services.AddTransient<IValidator<CustomEventDto>, CustomEventValidator>();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();
        }
    }
}
