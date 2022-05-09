using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
        public override DbSet<User> Users { get; set; }
        public DbSet<PostUserMapping> PostUserMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "Administrator".ToUpper()
                });

            modelBuilder.Entity<Post>().HasData(
                 new Post
                 {
                     Id =  Guid.NewGuid(),
                     Title = "New Car",
                     ImageUrl = "https://s1.cdn.autoevolution.com/images/models/AUDI_R8-V10-performance-RWD-2021_main.jpg",
                     Rating = 10,
                     CreatedAt = DateTime.UtcNow
                 });
        }
    }
}
