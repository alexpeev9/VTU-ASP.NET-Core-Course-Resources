namespace CatsProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Breed : BaseModel
    {
        public Breed()
        {
            this.Cats = new HashSet<Cat>();
        }
        [Required]
        public string Name { get; set; }
        public ICollection<Cat> Cats { get; set; }
    }

}
