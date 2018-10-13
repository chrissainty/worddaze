using System;
using System.Collections.Generic;
using System.Linq;
using WordDaze.Shared;

namespace WordDaze.Server
{
    public class BlogPostService
    {
        private List<BlogPost> _blogPosts;

        public BlogPostService()
        {
            _blogPosts = new List<BlogPost>();
        }

        public List<BlogPost> GetBlogPosts() 
        {
            return _blogPosts;
        }

        public BlogPost GetBlogPost(int id) 
        {
            return _blogPosts.SingleOrDefault(x => x.Id == id);
        }

        public BlogPost AddBlogPost(BlogPost newBlogPost)
        {
            newBlogPost.Id = _blogPosts.Count + 1;
            _blogPosts.Add(newBlogPost);

            return newBlogPost;
        }

        public void UpdateBlogPost(int postId, string updatedPost, string updateTitle)
        {
            var originalBlogPost = _blogPosts.Find(x => x.Id == postId);
            
            originalBlogPost.Post = updatedPost;
            originalBlogPost.Title = updateTitle;
        }

        public void DeleteBlogPost(int postId) 
        {
            var blogPost = _blogPosts.Find(x => x.Id == postId);

            _blogPosts.Remove(blogPost);
        }
    }
}