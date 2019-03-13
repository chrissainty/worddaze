using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;

namespace WordDaze.App.Features.Layout
{
    public class MainLayoutModel : LayoutComponentBase
    {
        [Inject] protected AppState AppState { get; set; }

        protected async Task Logout()
        {
            await AppState.Logout();
        }
    }
}