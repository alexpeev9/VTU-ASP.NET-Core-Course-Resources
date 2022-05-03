using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seed
{
	public class DbInitializer
	{
		private readonly static string adminId = "cbd958a7-bbc1-4b66-9085-f6a49a67b0fc";
		private readonly static string userId2 = "4bd958a7-bbc1-4b66-9085-f6a49a67b0fc";
		private readonly static string userRoleId = "1bd958a7-bac1-4b66-9085-f6a49a67b0fc";
		private readonly static string adminRoleId = "2bd958a7-bac1-4b66-1085-f6a49a67b0fc";
		public static void Seed(AppDbContext context)
		{
			if (!context.Roles.Any())
			{
				context.AddRange(
					new IdentityRole
					{
						Id = adminRoleId,
						Name = "Admin",
						NormalizedName = "ADMIN"
					},
					new IdentityRole
					{
						Id = userRoleId,
						Name = "User",
						NormalizedName = "USER"
					}
				);
			}
			if (!context.Users.Any())
			{
				context.AddRange(
					new User
					{
						Id = adminId,
						Email = "admin@abv.bg",
						UserName = "Admin Adminov",
						PasswordHash = new PasswordHasher<User>().HashPassword(null, "12345")
					},
					new User
					{
						Id = userId2,
						Email = "pesho@abv.bg",
						UserName = "Petar Petrov",
						PasswordHash = new PasswordHasher<User>().HashPassword(null, "pesho@abv.bg")
					}
				);
				if (!context.UserRoles.Any())
				{
					context.AddRange(
					new IdentityUserRole<string>
					{
						UserId = adminId,
						RoleId = adminRoleId
					},
					new IdentityUserRole<string>
					{
						UserId = userId2,
						RoleId = userRoleId
					}
					);
				}
				if (!context.Posts.Any())
				{
					context.AddRange(
						new Post
						{
							Title = "New Car",
							ImageUrl = "https://s1.cdn.autoevolution.com/images/models/AUDI_R8-V10-performance-RWD-2021_main.jpg",
							Rating = 10,
							CreatedAt = DateTime.UtcNow,
							UserId = adminId
						},
						new Post
						{
							Title = "Tsarevets",
							ImageUrl = "http://sofiaairporttransfer.com/wp-content/uploads/2014/12/Tsarevets-fortress.jpg",
							Rating = 5,
							CreatedAt = DateTime.UtcNow,
							UserId = userId2
						}
						);
				}
			}
			context.SaveChanges();
		}
	}
}
