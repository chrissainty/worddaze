using Microsoft.AspNetCore.Components;
using WordDaze.Shared;

namespace WordDaze.App.Features.Home
{
    public class BlogPostPreviewModel : ComponentBase
    {
        [Parameter] protected BlogPost blogPost { get; set; }
    }
}