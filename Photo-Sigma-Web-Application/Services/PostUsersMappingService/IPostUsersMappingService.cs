namespace Services.PostUsersMappingService
{
    using Models;
    using System;

    public interface IPostUsersMappingService
    {
        public void CreatePostUsersMapping(Guid postId, string userId);
        public bool HasVoted(Guid postId, string userId);
    }
}
