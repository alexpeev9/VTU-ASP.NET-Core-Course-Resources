using Data;
using Models;
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
		public PostService(AppDbContext appDbConext)
		{
			this.appDbContext = appDbConext;
		}
		public Post Create(Post post)
		{
			Post postDb = appDbContext.Posts.Add(post).Entity;
			appDbContext.SaveChanges();
			return postDb;
		}

	}
}
