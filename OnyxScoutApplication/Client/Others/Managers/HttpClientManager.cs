using System;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OnyxScoutApplication.Client.Others.Managers
{
    public class HttpClientManager
    {
        private readonly HttpClient httpClient;
        private readonly NotificationManager notificationService;

        public HttpClientManager(HttpClient httpClient, NotificationManager notificationService)
        {
            this.httpClient = httpClient;
            this.notificationService = notificationService;
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
        
        public async Task<bool> TryPostJson(string command, HttpContent content)
        {
            return await TrySetAsync(async () => await httpClient.PostAsync(command, content));
        }

        private async Task<T> TryGetAsync<T>(Func<Task<HttpResponseMessage>> action) where T : class
        {
            var response = await TryExecuteAsync(action);
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }
            string json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
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
                    await NotifyFailer(response);
                }
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            catch (Exception exception)
            {
                await NotifyFailer("Error", exception.Message);
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
            });
            return new StringContent(inputJson, Encoding.UTF8, "application/json");
        }
    }
}
