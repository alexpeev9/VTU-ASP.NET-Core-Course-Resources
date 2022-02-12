namespace Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public User()
        {
            this.PostUsersMapping = new HashSet<PostUsersMapping>();
        }
        public IEnumerable<PostUsersMapping> PostUsersMapping { get; set; }
    }
}
