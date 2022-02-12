namespace Services
{
    using Data;
    public class BaseService
    {
        public BaseService(AppDbContext dbContext)
        {
            this.AppDbContext = dbContext;
        }
        public AppDbContext AppDbContext { get; set; }
    }
}
