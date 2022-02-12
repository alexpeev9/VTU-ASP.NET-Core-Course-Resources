namespace Services.PostUsersMappingService
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;

    public class PostUsersMappingService : BaseService, IPostUsersMappingService
    {
        public PostUsersMappingService(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public void CreatePostUsersMapping(Guid postId, string userId)
        {
            Post post = GetPostById(postId);

            if (post.UserId == userId)
            {
                throw new Exception("User Cannot Vote for Himself.");
            }

            PostUsersMapping postUsersMapping = new PostUsersMapping();
            postUsersMapping.PostId = postId;
            postUsersMapping.UserId = userId;

            if (this.AppDbContext.PostsUsersMapping.Any(pu => (pu.UserId == userId) && (pu.PostId == postId)))
            {
                throw new Exception("User Cannot Vote Two Times.");
            }

            post.Rating += 1;
            this.AppDbContext.PostsUsersMapping.Add(postUsersMapping);
            this.AppDbContext.Posts.Update(post);
            this.AppDbContext.SaveChanges();
        }
        public Post GetPostById(Guid id) => this.AppDbContext.Posts.Include(p => p.User).FirstOrDefault(p => p.Id == id);
        public bool HasVoted(Guid postId, string userId)
        {
            if (this.AppDbContext.PostsUsersMapping.Any(pu => (pu.UserId == userId) && (pu.PostId == postId)))
            {
                return false;
            }
            return true;
        }
    }
}
