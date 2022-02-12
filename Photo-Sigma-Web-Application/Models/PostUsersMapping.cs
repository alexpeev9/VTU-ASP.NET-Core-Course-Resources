namespace Models
{
    using System;
    public class PostUsersMapping : BaseModel
    {
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
