using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using WordDaze.Shared;

namespace WordDaze.Client
{
    public class AppState
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public bool IsLoggedIn { get; private set; }

        public AppState(HttpClient httpClient,
                        ILocalStorageService localStorage)
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