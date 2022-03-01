namespace CatsProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Cat : BaseModel
    {
        //[Required]
        //[MinLength(3)]
        //[MaxLength(15)]
        [Required(ErrorMessage = "Name is Required.")]
        [StringLength(
            15,
            MinimumLength = 3,
            ErrorMessage ="Name must be between 3 and 15 characters long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Age is Required.")]
        [StringLength(
            15,
            MinimumLength = 1,
            ErrorMessage = "Age must be between 1 and 15 characters long")]
        public int Age { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Guid BreedId { get; set; }
        public Breed Breed { get; set; }
    }
}
