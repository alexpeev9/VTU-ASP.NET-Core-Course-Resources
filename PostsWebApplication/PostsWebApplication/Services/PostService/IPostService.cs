using PostsWebApplication.Database.Models;
using PostsWebApplication.DTOs.PostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsWebApplication.Services.PostService
{
    public interface IPostService
    {
        PostModel Create(PostCreateModel postCM);
        void Delete(Guid id);
        IList<PostModel> GetAll();
        PostModel Details(Guid id);
        PostUpdateModel GetEditModel(Guid id);
        PostModel Edit(Guid id, PostUpdateModel postUpdateModel);
    }
}
