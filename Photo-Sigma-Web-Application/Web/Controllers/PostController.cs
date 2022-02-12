namespace Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.PostService;
    using Services.PostUsersMappingService;
    using System;
    using System.Collections.Generic;

    public class PostController : Controller
    {
        public PostController(IPostService postService, IPostUsersMappingService postUsersMappingService, UserManager<User> userManager)
        {
            this.PostService = postService;
            this.PostUsersMappingService = postUsersMappingService;
            this.UserManager = userManager;
        }
        private readonly UserManager<User> UserManager;
        public IPostService PostService { get; set; }
        public IPostUsersMappingService PostUsersMappingService { get; set; }


        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([Bind ("Title", "ImageUrl", "Description")]Post post, string? errorMessage)
        {
            try
            {
                string userId = UserManager.GetUserId(User);
                if (this.ModelState.IsValid == false)
                {
                    this.View(post);
                }
                var createdPost = this.PostService.Create(post, userId);
                return RedirectToAction(nameof(this.Details), new { id = createdPost.Id });
            }
            catch (Exception exception)
            {
                return RedirectToAction(nameof(this.Create), new { errorMessage = exception.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(Guid id, string? errorMessage)
        {
            Post post = this.PostService.GetById(id);
            if(post == null)
            {
                return View("~/Views/Errors/404.cshtml");
            }
            ViewBag.ErrorMessage = errorMessage;
            return View(post);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit([Bind("Id","Title", "ImageUrl", "Description", "UserId")] Post post)
        {
            try
            {
                string userId = UserManager.GetUserId(User);
                if (this.ModelState.IsValid == false)
                {
                    this.View(post);
                }
                this.PostService.Update(post, userId);
                return RedirectToAction(nameof(this.Details), new { id = post.Id });
            }
            catch (Exception exception)
            {
                return RedirectToAction(nameof(this.Edit), new { id = post.Id, post = post, errorMessage = exception.Message });
            }
        }

        [HttpGet]
        public IActionResult Details(Guid id, string? errorMessage)
        {
            Post post = this.PostService.GetById(id);
            string userId = UserManager.GetUserId(User);
            if (post == null)
            {
                return View("~/Views/Errors/404.cshtml");
            }
            ViewBag.HasVoted = this.PostUsersMappingService.HasVoted(id, userId);
            ViewBag.ErrorMessage = errorMessage;
            return View(post);
        }

        [HttpGet]
        public IActionResult All(string? voteMessage)
        {
            ViewBag.VoteMessage = voteMessage;
            IEnumerable<Post> posts = this.PostService.GetAll();
            return View(posts);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {
                string userId = UserManager.GetUserId(User);
                this.PostService.Delete(id, userId);
                return RedirectToAction(nameof(this.All));
            }
            catch (Exception exception)
            {
                return RedirectToAction(nameof(this.Details), new { id = id, errorMessage = exception.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Vote(Guid id)
        {
            try
            {
                string userId = UserManager.GetUserId(User);
                this.PostUsersMappingService.CreatePostUsersMapping(id, userId);
                return RedirectToAction(nameof(this.All), new { voteMessage = "Successfully Voted!" });
            }
            catch (Exception exception)
            {
                return RedirectToAction(nameof(this.Details), new { id = id, errorMessage = exception.Message });
            }
        }
    }
}
