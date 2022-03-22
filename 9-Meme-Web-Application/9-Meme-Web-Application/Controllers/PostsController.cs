using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9_Meme_Web_Application.Controllers
{
	public class PostsController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Post post)
		{
			// TODO: Add Service
			return RedirectToAction(nameof(this.Index));
		}
	}
}
