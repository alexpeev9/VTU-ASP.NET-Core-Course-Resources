using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostsWebApplication.DTOs.PostDtos
{
    public class PostUpdateModel
    {

        [Required(ErrorMessage = "Description is Required!")]
        public string Description { get; set; }
    }
}
