using AutoMapper;
using PostsWebApplication.Database.Models;
using PostsWebApplication.DTOs.PostDtos;

namespace PostsWebApplication.DTOs
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Post, PostModel>();
            CreateMap<PostCreateModel, Post>();
            CreateMap<Post, PostUpdateModel>(); // for GET
            CreateMap<PostUpdateModel, Post>(); // for POST
        }
    }
}
