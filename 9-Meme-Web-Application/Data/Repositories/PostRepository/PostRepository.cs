using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.PostRepository
{
	 public class PostRepository : IPostRepository
	{
		private readonly AppDbContext appDbContext;
		public PostRepository(AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
		}

		public void CreatePost(Post post)
		{
			this.appDbContext.Posts.Add(post);
			this.appDbContext.SaveChanges();
		}
	}
}
