namespace CatsProject.Data
{
	using CatsProject.Data.Models;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Cat> Cats { get; set; }
		public DbSet<Breed> Breeds { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder
				.Entity<Cat>()
				.HasOne(c => c.Breed)
				.WithMany(b => b.Cats)
				.HasForeignKey(c => c.BreedId);

			builder.Entity<IdentityRole>().HasData(
			 new IdentityRole
			 {
				 Id = "e8b6fc47-ad33-486e-ac87-37d639562e71",
				 Name = "User",
				 NormalizedName = "User".ToUpper() // creates user role
			 },
			 new IdentityRole
			 {
				 Id = "r326fc47-ad33-486e-ac87-37d639562e71",
				 Name = "Administrator",
				 NormalizedName = "Administrator".ToUpper() // creates admin role
			 });;

			var user = new IdentityUser
			{
				Id = "18b6fc47-ad33-486e-ac87-37d639562e71", 
				UserName = "admin1@abv.bg", // creates user
			};

			user.PasswordHash = new PasswordHasher<IdentityUser>()
					.HashPassword(user, "1#AdminPassword"); // sets the password

			builder.Entity<IdentityUser>().HasData(user); // sets the user to db
			builder.Entity<IdentityUserRole<string>>().HasData(
				new IdentityUserRole<string>() {
				UserId = "18b6fc47-ad33-486e-ac87-37d639562e71", // id of user
				RoleId = "r326fc47-ad33-486e-ac87-37d639562e71" // id of admin role
				});
		}

		// or you can go to Areas->Identity->Pages->Account->Register.cshtml.cs
		// and add on 80th row - await _userManager.AddToRoleAsync(user, "User");
		// you can change it if you need to have administrator like this:
		// await _userManager.AddToRoleAsync(user, "Administrator");


	}
}
