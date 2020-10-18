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
using OnyxScoutApplication.Client.Others.Objects;

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
            services.AddHttpClient("OnyxScoutApplication.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
               .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("OnyxScoutApplication.ServerAPI"));

            services.AddApiAuthorization();

            services.AddSyncfusionBlazor();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjgzMDMyQDMxMzgyZTMxMmUzME1Ja1ptbzlHdFZQanM2REdUdkVxakd2ckJ4bG5sZy85N2dxZUJ3Nm15N3M9");
            services.AddTransient<HttpClientManager>();
            services.AddTransient<TeamDataAnalyzer>();
            services.AddSingleton<NotificationManager>();
            services.AddTransient<IValidator<ScoutFormFormatDto>, ScoutFormForamtValidator>();
            services.AddTransient<IValidator<FieldDto>, FieldValidator>();
            services.AddBlazoredLocalStorage();
        }
    }
}
