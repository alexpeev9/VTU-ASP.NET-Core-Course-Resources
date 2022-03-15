using Data;
using Models;
using Services.PostService;
using System;

namespace ConsoleTestApp
{
	public class Program
	{
		private readonly AppDbContext appDbContext;
		private readonly IPostService postService;
		public Program(AppDbContext appDbContext, IPostService postService)
		{
			this.appDbContext = appDbContext;
			this.postService = postService;
		}
		static void Main(string[] args)
		{
			// Doesn't Work only for Demo
			Post post = new Post();
			post.Title = "New Song";
			post.ImageUrl = "https:/.";

			Post postView = postService.Create(post);

			Console.WriteLine(postView.Title);
		}
	}
}
