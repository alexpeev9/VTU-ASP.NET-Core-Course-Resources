using AutoMapper;
using PostsWebApplication.Database;
using PostsWebApplication.Database.Models;
using PostsWebApplication.DTOs.PostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsWebApplication.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        public PostService(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public PostModel Create(PostCreateModel postCM)
        {
            //if(post.Author == null || post.Description == null)
            //{
            //    throw new Exception("Please fill all values");
            //}

            Post post = this.mapper.Map<Post>(postCM);

            this.dbContext.Posts.Add(post);
            this.dbContext.SaveChanges();

            PostModel postModel = this.mapper.Map<PostModel>(post);
            return postModel;
        }

        public void Delete(Guid id)
        {
            Post post = this.dbContext.Posts.Find(id);
            this.dbContext.Posts.Remove(post);
            this.dbContext.SaveChanges();
        }

        public IList<PostModel> GetAll()
        {
            List<Post> post = this.dbContext.Posts.ToList();
            List<PostModel> postModels = this.mapper.Map<List<PostModel>>(post);
            return postModels;
        }
        public PostModel Details(Guid id)
        {
            Post post = this.dbContext.Posts.Find(id);
            if(post == null)
            {
                throw new Exception("This post doesn't exist!");
            }
            PostModel postModel = this.mapper.Map<PostModel>(post);
            return postModel;
        }

        public PostUpdateModel GetEditModel(Guid id)
        {
            Post post = this.dbContext.Posts.Find(id);
            if(post == null)
            {
                throw new Exception("Post doesn't exist!");
            }
            PostUpdateModel postUpdateModel = this.mapper.Map<PostUpdateModel>(post);
            return postUpdateModel;
        }

        public PostModel Edit(Guid id, PostUpdateModel postUpdateModel)
        {
            Post post = this.dbContext.Posts.Find(id);
            if(post == null)
            {
                throw new Exception("Post doesn't exist!");
            }
            Post updatedModel = this.mapper.Map(postUpdateModel, post);
            this.dbContext.Update(updatedModel);
            this.dbContext.SaveChanges();
            PostModel postModel = this.mapper.Map<PostModel>(updatedModel);
            return postModel;
        }
    }
}
