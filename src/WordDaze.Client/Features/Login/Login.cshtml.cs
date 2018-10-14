using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using WordDaze.Shared;

namespace WordDaze.Client.Features.Login
{
    public class LoginModel : BlazorComponent
    {
        [Inject] private AppState _appState { get; set; }
        [Inject] private IUriHelper _uriHelper { get; set; }
        
        protected LoginDetails LoginDetails { get; set; } = new LoginDetails();
        protected bool ShowLoginFailed { get; set; }

        protected async Task Login()
        {
            await _appState.Login(LoginDetails);

            if (_appState.IsLoggedIn)
            {
                _uriHelper.NavigateTo("/");
            }
            else
            {
                ShowLoginFailed = true;
            }
        }
    }
}