using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Post
	{
		public Post()
		{
			this.Rating = 0;
			this.CreatedAt = DateTime.UtcNow;
		}

		[Key]
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string ImageUrl { get; set; }
		public int Rating { get; set; }
		public DateTime CreatedAt { get; set; }

		// One To Many Relation
		public string UserId { get; set; }
		public User User { get; set; }

		// Many To Many Relation
		public ICollection<PostUserMapping> PostUserMappings {get;set;}
	}
}
