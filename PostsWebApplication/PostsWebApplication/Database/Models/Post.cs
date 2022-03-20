namespace PostsWebApplication.Database.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Guid Id { get; set; }

        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
