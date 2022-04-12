using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		private readonly UserManager<User> userManager;
		public PostsController(AppDbContext appDbContext, IPostService postService, UserManager<User> userManager)
		{
			this.appDbContext = appDbContext;
			this.postService = postService;
			this.userManager = userManager;


		}
		[HttpGet]
		public IActionResult Index()
		{			
			List<Post> posts = this.appDbContext.Posts
										.Include( p => p.User)
										.Select( p => new Post() {
												Id = p.Id,
												Title = p.Title,
												ImageUrl = p.ImageUrl,
												Rating = p.Rating,
												UserId = p.UserId,
												User = new User()
												{
													Id = p.User.Id,
													UserName = p.User.UserName,
												}
										})
										.ToList();
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
			string userId = userManager.GetUserId(User);

			var post = this.appDbContext.Posts.Find(id);
			if(post == null)
			{
				return RedirectToAction(nameof(this.Index));
			}
			if (post.UserId != userId)
			{
				return RedirectToAction(nameof(this.Index));
			}

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
			string userId = userManager.GetUserId(User);
			this.postService.Create(post, userId);
			return RedirectToAction(nameof(this.Index));
		}

		[HttpGet]
		public IActionResult UpVote(Guid id)
		{
			var userId = this.userManager.GetUserId(User);
			var post = this.appDbContext.Posts.Find(id);
			if(post.UserId == userId)
			{
				return RedirectToAction(nameof(this.Create));
			}
			var isVoted = this.appDbContext.PostUserMappings.Where(pum => pum.PostId == post.Id).Where(pum => pum.UserId == userId).SingleOrDefault();
			if(isVoted != null)
			{
				return RedirectToAction(nameof(this.Create));
			}
			var vote = new PostUserMapping()
			{
				UserId = userId,
				PostId = post.Id
			};
			this.appDbContext.PostUserMappings.Add(vote);

			post.Rating += 1;
			this.appDbContext.Update(post);
			this.appDbContext.SaveChanges();
			return RedirectToAction(nameof(this.Index));
		}

		[HttpGet]
		public IActionResult DownVote(Guid id)
		{
			var userId = this.userManager.GetUserId(User);
			var post = this.appDbContext.Posts.Find(id);
			if (post.UserId == userId)
			{
				return RedirectToAction(nameof(this.Create));
			}
			var isVoted = this.appDbContext.PostUserMappings.Where(pum => pum.PostId == post.Id).Where(pum => pum.UserId == userId).SingleOrDefault();
			if (isVoted != null)
			{
				return RedirectToAction(nameof(this.Create));
			}
			var vote = new PostUserMapping()
			{
				UserId = userId,
				PostId = post.Id
			};
			this.appDbContext.PostUserMappings.Add(vote);

			post.Rating -= 1;
			this.appDbContext.Update(post);
			this.appDbContext.SaveChanges();
			return RedirectToAction(nameof(this.Index));
		}
	}
}
