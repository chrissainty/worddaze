using Microsoft.AspNetCore.Components;
using WordDaze.Shared;

namespace WordDaze.Client.Features.Home
{
    public class BlogPostPreviewModel : ComponentBase
    {
        [Parameter] public BlogPost blogPost { get; set; }
    }
}