using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WordDaze.Shared;


namespace WordDaze.Client.Features.Login
{
    public class LoginModel : ComponentBase
    {
        [Inject] private AppState _appState { get; set; }
        [Inject] private NavigationManager _navManager { get; set; }
        
        protected LoginDetails LoginDetails { get; set; } = new LoginDetails();
        protected bool ShowLoginFailed { get; set; }

        protected async Task Login()
        {
            await _appState.Login(LoginDetails);

            if (_appState.IsLoggedIn)
            {
                _navManager.NavigateTo("/");
            }
            else
            {
                ShowLoginFailed = true;
            }
        }
    }
}