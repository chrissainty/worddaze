using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.JSInterop;
using WordDaze.Shared;

namespace WordDaze.Client.Features.AddPost
{
    public class AddPostModel : BlazorComponent
    {
        [Inject] private HttpClient _httpClient { get; set; }
        [Inject] private IUriHelper _uriHelper { get; set; }

        protected string Post { get; set; }
        protected string Title { get; set; }
        protected int CharacterCount { get; set; }

        protected ElementRef editor;

        public async Task UpdateCharacterCount() => CharacterCount = await JSRuntime.Current.InvokeAsync<int>("wordDaze.getCharacterCount", editor);

        public async Task SavePost() 
        {
            var newPost = new BlogPost() {
                Title = Title,
                Author = "Joe Bloggs",
                Post = Post,
                Posted = DateTime.Now
            };

            var savedPost = await _httpClient.PostJsonAsync<BlogPost>(Urls.AddBlogPost, newPost);

            _uriHelper.NavigateTo($"viewpost/{savedPost.Id}");
        }
    }
}