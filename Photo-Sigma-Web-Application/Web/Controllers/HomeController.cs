using Microsoft.AspNetCore.Mvc;
using Models;
using Services.PostService;
using System.Collections.Generic;
using System.Diagnostics;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(IPostService postService)
        {
            this.PostService = postService;
        }
        public IPostService PostService { get; set; }

        public IActionResult Index()
        {
            IEnumerable<Post> posts = this.PostService.GetAll();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/NotFound")]
        public IActionResult PageNotFound()
        {
                return View("~/Views/Errors/404.cshtml");
        }
    }
}
