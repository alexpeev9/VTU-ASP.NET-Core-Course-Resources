namespace BookProject.Data.Models.Abstraction
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
