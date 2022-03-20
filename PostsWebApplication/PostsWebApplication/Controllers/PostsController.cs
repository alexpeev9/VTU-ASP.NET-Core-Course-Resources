using Microsoft.AspNetCore.Mvc;
using PostsWebApplication.Database.Models;
using PostsWebApplication.DTOs.PostDtos;
using PostsWebApplication.Services.PostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsWebApplication.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            try
            {
                PostModel post = this.postService.Details(id);
                return View(post);
            }
            catch(Exception exception)
            {
                return RedirectToAction(nameof(this.Index), new { errorMessage = exception.Message });
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                PostUpdateModel postModel = this.postService.GetEditModel(id); 
                return View(postModel);
            }
            catch (Exception exception)
            {
                return RedirectToAction(nameof(this.Index), new { errorMessage = exception.Message });
            }
        }

        [HttpPost]
        public IActionResult Edit(Guid id, PostUpdateModel postUpdateModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(postUpdateModel);
            }
            try
            {
                PostModel postModel = this.postService.Edit(id, postUpdateModel);
                return RedirectToAction(nameof(this.Details), new { id = postModel.Id });
            }
            catch (Exception exception)
            {
                return RedirectToAction(nameof(this.Index), new { errorMessage = exception.Message });
            }
        }

        [HttpGet]
        public IActionResult Index(string? errorMessage)
        {
            try
            {
                IList<PostModel> postModels = this.postService.GetAll();
                ViewBag.ErrorMessage = errorMessage;
                return View(postModels);
            }
            catch(Exception exception)
            {
                return RedirectToAction(nameof(this.Create), new { errorMessage = exception.Message });
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.postService.Delete(id);
                return RedirectToAction(nameof(this.Index));
            }
            catch (Exception exception)
            {
                return RedirectToAction(nameof(this.Index), new { errorMessage = exception.Message });
            }
        }

        [HttpGet]
        public IActionResult Create(string? errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        [HttpPost]
        public IActionResult Create(PostCreateModel postCM)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(postCM);
            }

            try
            {
                PostModel postModel = this.postService.Create(postCM);
                return RedirectToAction(nameof(this.Details), new { id = postModel.Id});
            }
            catch(Exception exception)
            {
                return RedirectToAction(nameof(this.Create), new { errorMessage = exception.Message });
            }
        }
    }
}
