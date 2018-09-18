using System;
using System.Net.Http;
using System.Threading.Tasks;
using Markdig;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using WordDaze.Shared;

namespace WordDaze.Client.Features.ViewPost
{
    public class ViewPostModel : BlazorComponent 
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