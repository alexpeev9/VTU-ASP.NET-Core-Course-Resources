namespace CatsProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Cat : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Guid BreedId { get; set; }
        public Breed Breed { get; set; }
    }
}
