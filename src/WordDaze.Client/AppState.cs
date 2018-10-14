using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Blazor;
using Microsoft.JSInterop;
using WordDaze.Shared;
using System.Threading.Tasks;
using Blazored.Storage;
using System;
using System.Net.Http.Headers;
using System.Net;

namespace WordDaze.Client
{
    public class AppState
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorage _localStorage;

        public bool IsLoggedIn { get; private set; }

        public AppState(HttpClient httpClient,
                        ILocalStorage localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        
        public async Task Login(LoginDetails loginDetails)
        {
            var response = await _httpClient.PostAsync(Urls.Login, new StringContent(Json.Serialize(loginDetails), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                await SaveToken(response);
                await SetAuthorizationHeader();

                IsLoggedIn = true;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItem("authToken");
            IsLoggedIn = false;
        }

        private async Task SaveToken(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jwt = Json.Deserialize<JwToken>(responseContent);
            
            await _localStorage.SetItem("authToken", jwt.Token);
        }

        private async Task SetAuthorizationHeader()
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                var token = await _localStorage.GetItem<string>("authToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}