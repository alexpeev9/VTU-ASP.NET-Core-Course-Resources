using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostsWebApplication.DTOs.PostDtos
{
    public class PostCreateModel
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
