using Data;
using Data.Repositories.PostRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PostService
{
	public class PostService : IPostService
	{
		private readonly AppDbContext appDbContext;
		//private readonly PostRepository postRepository;
		// if you are using repository pattern, there should be no appDbContext in services
		public PostService(AppDbContext appDbConext /*PostRepository postRepository */)
		{
			this.appDbContext = appDbConext;
			//this.postRepository = postRepository;
		}

		public void Create(Post post, String userId)
		{
			post.UserId = userId;
			//this.postRepository.CreatePost(post);
			this.appDbContext.Posts.Add(post);
			this.appDbContext.SaveChanges();
		}

		public void Delete(Guid id)
		{
			var post = this.appDbContext.Posts.Find(id);
			this.appDbContext.Posts.Remove(post);
			this.appDbContext.SaveChanges();
		}
		public List<PostVM> GetAllPosts (string userId)
		{
			return this.appDbContext.Posts
										.Include(p => p.User)
										.Include(p => p.PostUserMappings)
										.Select(p => new PostVM()
										{
											Id = p.Id,
											Title = p.Title,
											ImageUrl = p.ImageUrl,
											Rating = p.Rating,
											UserId = p.UserId,
											User = new User()
											{
												Id = p.User.Id,
												UserName = p.User.UserName,
											},
											HasVoted = appDbContext.PostUserMappings.Any(pu => (pu.UserId == userId) && (pu.PostId == p.Id))
										})
										.ToList(); ;
		}
	}
}
