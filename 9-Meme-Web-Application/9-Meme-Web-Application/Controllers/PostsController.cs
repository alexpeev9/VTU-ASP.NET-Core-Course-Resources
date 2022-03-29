using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.PostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9_Meme_Web_Application.Controllers
{
	public class PostsController : Controller
	{
		private readonly AppDbContext appDbContext;
		private readonly IPostService postService;
		public PostsController(AppDbContext appDbContext, IPostService postService)
		{
			this.appDbContext = appDbContext;
			this.postService = postService;
		
		}
		[HttpGet]
		public IActionResult Index()
		{			
			List<Post> posts = this.appDbContext.Posts.ToList();
			return View(posts);
		}

		[HttpGet]
		public IActionResult Details(Guid id)
		{
			Post post = this.appDbContext.Posts.Find(id);
			if(post == null)
			{
				return RedirectToAction(nameof(this.Index));
			}
			return View(post);
		}

		[HttpGet]
		public IActionResult Delete(Guid id)
		{
			var post = this.appDbContext.Posts.Find(id);
			this.appDbContext.Posts.Remove(post);
			this.appDbContext.SaveChanges();
			return RedirectToAction(nameof(this.Index));

		}

		[HttpGet]
		public IActionResult Edit(Guid id)
		{
			var post = this.appDbContext.Posts.Find(id);

			return View(post);
		}

		[HttpPost]
		public IActionResult Edit(Post post)
		{
			this.appDbContext.Update(post);
			this.appDbContext.SaveChanges();
			return RedirectToAction(nameof(this.Index));
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Post post)
		{
			this.postService.Create(post);
			return RedirectToAction(nameof(this.Index));
		}
	}
}
