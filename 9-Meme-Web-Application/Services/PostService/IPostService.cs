using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PostService
{
	public interface IPostService
	{
		public Post Create(Post post);
	}
}
