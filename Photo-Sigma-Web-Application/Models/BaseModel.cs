namespace Models
{
    using System;
    public class BaseModel
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
