using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatsProject.Data;
using CatsProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CatsProject.Controllers
{
    public class CatsController : Controller
    {
        private readonly ApplicationDbContext _context;
		public CatsController(ApplicationDbContext context)
        {
            _context = context;

		}

		// GET: Cats
		public async Task<IActionResult> Index(string? errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            var applicationDbContext = _context.Cats.Include(c => c.Breed);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cats/Details/5
        public async Task<IActionResult> Details(Guid? id, string? errorMessage)
        {
            try
            {
                if( id == null)
                {
                    throw new Exception("Id not Defined!");
                }
                var cat = await _context.Cats
                    .Include(c => c.Breed)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if( cat == null)
                {
                    throw new Exception("Cat Not Found!");
                }
                return View(cat);
            }
            catch(Exception exc)
            {
                return RedirectToAction(nameof(this.Index), new { errorMessage = exc.Message });
            }
        }

        // GET: Cats/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Name");
            return View();
        }

        // POST: Cats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Administrator")] // allows only admins to create cats
		public async Task<IActionResult> Create([Bind("Name,Age,ImageUrl,BreedId,Id")] Cat cat)
        {
            if (ModelState.IsValid)
            {
                cat.Id = Guid.NewGuid();
                _context.Add(cat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Name", cat.BreedId);
            return View(cat);
        }

        // GET: Cats/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Name", cat.BreedId);
            return View(cat);
        }

        // POST: Cats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Age,ImageUrl,BreedId,Id")] Cat cat)
        {
            if (id != cat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatExists(cat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Name", cat.BreedId);
            return View(cat);
        }

        // GET: Cats/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats
                .Include(c => c.Breed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // POST: Cats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cat = await _context.Cats.FindAsync(id);
            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatExists(Guid id)
        {
            return _context.Cats.Any(e => e.Id == id);
        }
    }
}
