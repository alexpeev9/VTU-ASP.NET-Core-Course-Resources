namespace Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post : BaseModel
    {
        public Post()
        {
            this.Rating = 0;
            this.PostUsersMappings = new HashSet<PostUsersMapping>();
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Description { get; set; }
        public int Rating { get; set; }
        public IEnumerable<PostUsersMapping> PostUsersMappings { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
