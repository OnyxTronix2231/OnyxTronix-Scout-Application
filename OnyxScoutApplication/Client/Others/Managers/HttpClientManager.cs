using System;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OnyxScoutApplication.Client.Others.Managers
{
    public class HttpClientManager
    {
        private readonly HttpClient httpClient;
        private readonly NotificationManager notificationService;
        private readonly AppManager appManager;
        private readonly ILocalStorageService localStorageService;

        public HttpClientManager(HttpClient httpClient, NotificationManager notificationService, AppManager appManager,
            ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.notificationService = notificationService;
            this.appManager = appManager;
            this.localStorageService = localStorageService;
        }
        
        public async Task<T> GetJsonByJsonText<T>(string command) where T : class
        {
            return await TryGetAsyncByJsonText<T>(async () => await httpClient.GetAsync(command));
        }

        public async Task<T> GetJson<T>(string command) where T : class
        {
            return await TryGetAsync<T>(async () => await httpClient.GetAsync(command));
        }
        
        public async Task<bool> TryPutJson(string command, object objectToPut)
        {
            return await TrySetAsync(async () => await httpClient.PutAsync(command, Serialize(objectToPut)));
        }

        public async Task<bool> TryPostJson(string command, object objectToPost)
        {
            return await TrySetAsync(async () => await httpClient.PostAsync(command, Serialize(objectToPost)));
        }
        
        public async Task<T> TryPostJson<T>(string command, object objectToPost) where T : class
        {
            return await TryGetAsync<T>(async () => await httpClient.PostAsJsonAsync(command, objectToPost));
        }
        
        public async Task<bool> TryPostJson(string command, HttpContent content) 
        {
            return await TrySetAsync(async () => await httpClient.PostAsync(command, content));
        }

        private async Task<T> TryGetAsync<T>(Func<Task<HttpResponseMessage>> action) where T : class
        {
            var response = await TryExecuteAsync(action);
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return null;
            }
            string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            var result = JsonConvert.DeserializeObject<T>(json,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Auto
                });
            return result;
        }
        
        private async Task<T> TryGetAsyncByJsonText<T>(Func<Task<HttpResponseMessage>> action) where T : class
        {
            var response = await TryExecuteAsync(action);
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }
            string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);

            var result = await response.Content.ReadFromJsonAsync<T>();
            return result;
        }
        
        private async Task<bool> TrySetAsync(Func<Task<HttpResponseMessage>> action)
        {
            var response = await TryExecuteAsync(action);
            if(response.IsSuccessStatusCode)
            {
                await notificationService.NotifyAsync("Success", "Pushed successfully", NotificationType.Success);
            }
            return response.IsSuccessStatusCode;
        }
        
        private async Task<HttpResponseMessage> TryExecuteAsync(Func<Task<HttpResponseMessage>> action)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await action();
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Real error code {response.StatusCode}");
                    if (response.StatusCode is HttpStatusCode.ServiceUnavailable or 
                        //HttpStatusCode.InternalServerError or
                        0)
                    {
                        appManager.IsOnlineMode = false;
                    }
                    await NotifyFailer(response);
                }
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                Console.WriteLine("AccessTokenNotAvailableException");
            }
            catch (Exception exception)
            {
                await NotifyFailer("Error", exception.Message);
                Console.WriteLine($"Error{exception.Message}");
                if (exception.Message.Contains("Failed to fetch"))
                {
                    appManager.IsOnlineMode = false;
                }
            }
            return response ?? new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
        }

        private async Task NotifyFailer(HttpResponseMessage response)
        {
            await notificationService.NotifyAsync($"Error: {response.StatusCode}", await response.Content.ReadAsStringAsync(),
                NotificationType.Danger);
        }
         
        private async Task NotifyFailer(string title, string message)
        {
            await notificationService.NotifyAsync(title, message, NotificationType.Danger);
        }

        private static HttpContent Serialize(object objectToSerialize)
        {
            var inputJson = JsonConvert.SerializeObject(objectToSerialize, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            });
            return new StringContent(inputJson, Encoding.UTF8, "application/json");
        }
    }
}
