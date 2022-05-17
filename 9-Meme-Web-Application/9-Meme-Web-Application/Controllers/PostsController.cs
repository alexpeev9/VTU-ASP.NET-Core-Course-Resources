using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
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
		public IActionResult Index(string? message)
		{
			string userId = userManager.GetUserId(User);
			ViewBag.ErrorMessage = message;
			List<PostVM> posts = this.postService.GetAllPosts(userId);
			return View(posts);
		}
		private bool HasUserVoted(string userId, Guid postId)
		{
			return appDbContext.PostUserMappings.Any(pu => (pu.UserId == userId) && (pu.PostId == postId));
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
			this.postService.Delete(id);
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
		//[Authorize(Roles = "Administrator")] // only admins
		//[Authorize] // only registered user
		//[AllowAnonymous] // everyone
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
				return RedirectToAction(nameof(this.Index), new { message = "You cannot vote for your post!" });
			}
			var isVoted = this.appDbContext.PostUserMappings.Where(pum => pum.PostId == post.Id).Where(pum => pum.UserId == userId).SingleOrDefault();
			if(isVoted != null)
			{
				return RedirectToAction(nameof(this.Index), new { message= "You have already voted!"});
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
				return RedirectToAction(nameof(this.Index), new { message = "You cannot vote for your post!" });
			}
			var isVoted = this.appDbContext.PostUserMappings.Where(pum => pum.PostId == post.Id).Where(pum => pum.UserId == userId).SingleOrDefault();
			if (isVoted != null)
			{
				return RedirectToAction(nameof(this.Index), new { message = "You have already voted!" });
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
