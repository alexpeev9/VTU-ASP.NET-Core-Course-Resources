namespace BookProject.Data
{
    using BookProject.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
	    protected override void OnModelCreating(ModelBuilder modelBuilder)
	    {
		    base.OnModelCreating(modelBuilder);

		    modelBuilder
			    .Entity<Book>()
			    .HasOne(u => u.Genre)
			    .WithMany(g => g.Books)
			    .HasForeignKey(u => u.GenreId);
	    }
    }
}
