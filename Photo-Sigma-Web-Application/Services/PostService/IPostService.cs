namespace Services.PostService
{
    using Models;
    using System;
    using System.Collections.Generic;
    public interface IPostService
    {
        public IEnumerable<Post> GetAll();
        public IEnumerable<Post> GetAllSorted();
        public Post Create(Post post, string userId);
        public Post GetById(Guid id);
        public Post Update(Post post, string userId);
        public void Delete(Guid Id, string userId);
    }
}
