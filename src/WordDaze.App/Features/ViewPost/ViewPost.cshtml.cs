using System.Net.Http;
using System.Threading.Tasks;
using Markdig;
using Microsoft.AspNetCore.Components;
using WordDaze.Shared;

namespace WordDaze.App.Features.ViewPost
{
    public class ViewPostModel : ComponentBase
    {
        [Inject] private HttpClient _httpClient { get; set; }

        [Parameter] protected string PostId { get; set; }

        protected BlogPost BlogPost { get; set; } = new BlogPost();

        protected override async Task OnInitAsync()
        {
            await LoadBlogPost();
        }

        private async Task LoadBlogPost()
        {
            BlogPost = await _httpClient.GetJsonAsync<BlogPost>(Urls.BlogPost.Replace("{id}", PostId));
            BlogPost.Post = Markdown.ToHtml(BlogPost.Post);
        }
    }
}