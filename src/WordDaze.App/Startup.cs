using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using WordDaze.App.Services;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Services;
using System;

namespace WordDaze.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>(s =>
            {
                return new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:64838/")
                };
            });

            services.AddSingleton<BlogPostService>();
            services.AddBlazoredLocalStorage();

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
