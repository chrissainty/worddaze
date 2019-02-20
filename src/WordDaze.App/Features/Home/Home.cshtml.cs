using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WordDaze.Shared;
using Microsoft.AspNetCore.Components;

namespace WordDaze.App.Features.Home
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