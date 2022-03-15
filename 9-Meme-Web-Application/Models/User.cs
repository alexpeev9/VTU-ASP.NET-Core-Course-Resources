namespace Models
{
	using Microsoft.AspNetCore.Identity;
	using System.Collections.Generic;

	public class User : IdentityUser
	{
		// Many to One
		public ICollection<Post> Posts { get; set; } // list of created posts

		// Many To Many
		public ICollection<PostUserMapping> PostUserMappings { get; set; } // list of voted posts
	}
}
