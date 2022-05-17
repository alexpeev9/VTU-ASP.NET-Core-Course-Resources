using Data;
using Data.Repositories.PostRepository;
using Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Services.PostService;
using Services.VoteService;
using System;

namespace _9_Meme_Web_Application
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddIdentity<User, IdentityRole>()
						  .AddEntityFrameworkStores<AppDbContext>()
						  .AddDefaultUI()
						  .AddDefaultTokenProviders();

			services.AddControllersWithViews();

			//services.AddTransient<IPostRepository, PostRepository>();

			services.AddTransient<IVoteService, VoteService>();
			services.AddTransient<IPostService, PostService>();
		}

		public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});

			// DbInitializer.Seed(serviceProvider.GetRequiredService<AppDbContext>());
		}
	}
}
