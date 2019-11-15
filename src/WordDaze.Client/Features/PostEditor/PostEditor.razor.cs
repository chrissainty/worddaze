using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WordDaze.Shared;


namespace WordDaze.Client.Features.PostEditor
{
    public class PostEditorModel : ComponentBase
    {
        [Inject] private HttpClient _httpClient { get; set; }
        [Inject] private NavigationManager _navManager { get; set; }
        [Inject] private AppState _appState { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        [Parameter] public string PostId { get; set; }

        protected string Post { get; set; }
        protected string Title { get; set; }
        protected int CharacterCount { get; set; }
        protected BlogPost ExistingBlogPost { get; set; } = new BlogPost();
        protected bool IsEdit => string.IsNullOrEmpty(PostId) ? false : true;

        protected ElementReference editor;

        protected override async Task OnInitializedAsync()
        {
            if (!_appState.IsLoggedIn) 
            {
                _navManager.NavigateTo("/");
            }

            if (!string.IsNullOrEmpty(PostId))
            {
                await LoadPost();
            }
        }

        public async Task UpdateCharacterCount() => CharacterCount = await JSRuntime.InvokeAsync<int>("wordDaze.getCharacterCount", editor);

        public async Task SavePost() 
        {
            var newPost = new BlogPost() {
                Title = Title,
                Post = Post,
                Posted = DateTime.Now
            };

            var savedPost = await _httpClient.PostJsonAsync<BlogPost>(Urls.AddBlogPost, newPost);

            _navManager.NavigateTo($"viewpost/{savedPost.Id}");
        }

        public async Task UpdatePost() 
        {
            await _httpClient.PutJsonAsync(Urls.UpdateBlogPost.Replace("{id}", PostId), ExistingBlogPost);

            _navManager.NavigateTo($"viewpost/{ExistingBlogPost.Id}");
        }

        public async Task DeletePost() 
        {
            await _httpClient.DeleteAsync(Urls.DeleteBlogPost.Replace("{id}", ExistingBlogPost.Id.ToString()));

            _navManager.NavigateTo("/");
        }

        private async Task LoadPost() 
        {
            ExistingBlogPost = await _httpClient.GetJsonAsync<BlogPost>(Urls.BlogPost.Replace("{id}", PostId));
            CharacterCount = ExistingBlogPost.Post.Length;
        }
    }
}