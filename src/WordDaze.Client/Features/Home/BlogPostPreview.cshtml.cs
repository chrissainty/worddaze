using Microsoft.AspNetCore.Blazor.Components;
using WordDaze.Shared;

namespace WordDaze.Client.Features.Home
{
    public class BlogPostPreviewModel : BlazorComponent 
    {
        [Parameter] protected BlogPost blogPost { get; set; }
    }
}