using BookProject.Data.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookProject.Data.Models
{
    public class Book : BaseModel
    {
        public Book()
        {
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
