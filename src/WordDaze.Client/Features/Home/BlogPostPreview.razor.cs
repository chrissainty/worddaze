using Microsoft.AspNetCore.Components;
using WordDaze.Shared;

namespace WordDaze.Client.Features.Home
{
    public class BlogPostPreviewModel : ComponentBase
    {
        [Parameter] protected BlogPost blogPost { get; set; }
    }
}