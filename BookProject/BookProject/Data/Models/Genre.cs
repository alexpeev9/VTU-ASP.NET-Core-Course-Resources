using BookProject.Data.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookProject.Data.Models
{
    public class Genre : BaseModel
    {
        public Genre()
        {
            this.Books = new HashSet<Book>();
        }
        [Required]
        public string Title { get; set; }
        public ICollection<Book> Books { get; set; }

    }
}
