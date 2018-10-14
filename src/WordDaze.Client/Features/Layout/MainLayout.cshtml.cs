using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.Services;

namespace WordDaze.Client.Features.Layout
{
    public class MainLayoutModel : BlazorLayoutComponent
    {
        [Inject] protected AppState AppState { get; set; }

        protected async Task Logout()
        {
            await AppState.Logout();
        }
    }
}