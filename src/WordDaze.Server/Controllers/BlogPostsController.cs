using WordDaze.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WordDaze.Server.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly BlogPostService _blogPostService;

        public BlogPostsController(BlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet(Urls.BlogPosts)]
        public IActionResult GetBlogPosts()
        {
            return Ok(_blogPostService.GetBlogPosts());
        }

        [HttpGet(Urls.BlogPost)]
        public IActionResult GetBlogPostById(int id)
        {
            var blogPost = _blogPostService.GetBlogPost(id);

            if (blogPost == null)
                return NotFound();

            return Ok(blogPost);
        }

        [Authorize]
        [HttpPost(Urls.AddBlogPost)]
        public IActionResult AddBlogPost([FromBody]BlogPost newBlogPost)
        {
            newBlogPost.Author = Request.HttpContext.User.Identity.Name;
            var savedBlogPost = _blogPostService.AddBlogPost(newBlogPost);

            return Created(new Uri(Urls.BlogPost.Replace("{id}", savedBlogPost.Id.ToString()), UriKind.Relative), savedBlogPost);
        }

        [Authorize]
        [HttpPut(Urls.UpdateBlogPost)]
        public IActionResult UpdateBlogPost(int id, [FromBody]BlogPost updatedBlogPost)
        {
            _blogPostService.UpdateBlogPost(id, updatedBlogPost.Post, updatedBlogPost.Title);

            return Ok();
        }

        [Authorize]
        [HttpDelete(Urls.DeleteBlogPost)]
        public IActionResult DeleteBlogPost(int id)
        {
            _blogPostService.DeleteBlogPost(id);

            return Ok();
        }
    }
}
