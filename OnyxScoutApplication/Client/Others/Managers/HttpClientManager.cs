using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(command);
                T result = null;
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    notificationService.Notify("Error fetching data", await response.Content.ReadAsStringAsync(), NotificationType.Danger);
                }
                return result;
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            return null;
        }

        public async Task<bool> TryPutJson(string command, object objectToPut)
        {
            try
            {
                var respons = await httpClient.PutAsync(command, Serialize(objectToPut));
                if (!respons.IsSuccessStatusCode)
                {
                    notificationService.Notify("Error", await respons.Content.ReadAsStringAsync(), NotificationType.Danger);
                }
                else
                {
                    notificationService.Notify("Success", "Updated successfully", NotificationType.Success);
                }
                return respons.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            return false;
        }

        public async Task<bool> TryPostJson(string command, object objectToPopst)
        {
            try
            {
                var respons = await httpClient.PostAsync(command, Serialize(objectToPopst));
                if (!respons.IsSuccessStatusCode)
                {
                    notificationService.Notify("Error", await respons.Content.ReadAsStringAsync(), NotificationType.Danger);
                }
                else
                {
                    notificationService.Notify("Success", "Created successfully", NotificationType.Success);
                }
                return respons.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            return false;
        }

        private HttpContent Serialize(object objectToSerialize)
        {
            var inputJson = JsonSerializer.Serialize(objectToSerialize);
            return new StringContent(inputJson, Encoding.UTF8, "application/json");
        }
    }
}
