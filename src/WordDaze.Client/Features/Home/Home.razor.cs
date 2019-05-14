using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.AspNetCore.Blazor;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WordDaze.Shared;
using System.Linq;

namespace WordDaze.Client.Features
{
    public class HomeModel : ComponentBase
    {
        [Inject] private HttpClient _httpClient { get; set; }

        protected List<BlogPost> blogPosts { get; set; } = new List<BlogPost>();

        protected override async Task OnInitAsync()
        {
            await LoadBlogPosts();
        }

        private async Task LoadBlogPosts()
        {
            var blogPostsResponse = await _httpClient.GetJsonAsync<List<BlogPost>>(Urls.BlogPosts);
            blogPosts = blogPostsResponse.OrderByDescending(p => p.Posted).ToList();
        }
    }
}