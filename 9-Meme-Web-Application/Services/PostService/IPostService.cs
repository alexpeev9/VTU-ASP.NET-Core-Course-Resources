using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PostService
{
	public interface IPostService
	{
		void Create(Post post, string userId);
		void Delete(Guid id);
		List<PostVM> GetAllPosts(string userId);
	}
}
