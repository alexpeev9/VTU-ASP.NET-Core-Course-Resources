namespace Services.PostService
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;
    using System;

    public class PostService : BaseService, IPostService
    {
        public PostService(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public IEnumerable<Post> GetAll() => this.AppDbContext.Posts.ToArray();
        public Post GetById(Guid id) => this.AppDbContext.Posts.Include(p => p.User)
                                    .Where(p => p.Id == id)
                                    .Select(p => new Post
                                    {
                                        Id = p.Id,
                                        Title = p.Title,
                                        Description = p.Description,
                                        ImageUrl = p.ImageUrl,
                                        Rating = p.Rating,
                                        UserId = p.UserId,
                                        User = new User
                                        {
                                            Id = p.User.Id,
                                            Email = p.User.Email
                                        }
                                    })
                                    .SingleOrDefault();
        public IEnumerable<Post> GetAllSorted() => this.AppDbContext.Posts.OrderBy(p => p.Rating).ToArray();
        public Post Create(Post post, string userId)
        {
            post.UserId = userId;
            this.AppDbContext.Posts.Add(post);
            this.AppDbContext.SaveChanges();

            return post;
        }
        public Post Update(Post post, string userId)
        {
            Post postDb = this.GetById(post.Id);
            if (postDb.UserId != userId)
            {
                throw new Exception("Only owner of Posts can edit post.");
            }
            post.UserId = postDb.UserId;
            post.Rating = postDb.Rating;

            this.AppDbContext.Posts.Update(post);
            this.AppDbContext.SaveChanges();

            return post;
        }
        public void Delete(Guid postId, string userId)
        {
            Post post = this.GetById(postId);
            if (post.UserId != userId)
            {
                throw new Exception("Only owner of Posts can delete post.");
            }
            this.AppDbContext.Posts.Remove(post);
            this.AppDbContext.SaveChanges();
        }
    }
}
