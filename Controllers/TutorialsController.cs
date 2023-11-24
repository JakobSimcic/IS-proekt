using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bojan_recipe.Data;
using bojan_recipe.Models;
using Microsoft.AspNetCore.Authorization;

namespace bojan_recipe.Controllers
{
    public class TutorialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TutorialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tutorials
        public async Task<IActionResult> Index()
        {
              return _context.Tutorial != null ? 
                          View(await _context.Tutorial.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tutorial'  is null.");
        }

        // GET: Tutorials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tutorial == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorial
                .FirstOrDefaultAsync(m => m.TutorialId == id);
            if (tutorial == null)
            {
                return NotFound();
            }

            return View(tutorial);
        }

        // GET: Tutorials/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.TutorialCategoryList = new SelectList(Enum.GetValues(typeof(TutorialCategory)).Cast<TutorialCategory>());
            return View();
        }

        // POST: Tutorials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("TutorialId,TutorialName,TutorialDescription,TutorialVideoUrl,Category")] Tutorial tutorial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorial);
        }

        // GET: Tutorials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tutorial == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorial.FindAsync(id);
            if (tutorial == null)
            {
                return NotFound();
            }
            ViewBag.TutorialCategoryList = new SelectList(Enum.GetValues(typeof(TutorialCategory)).Cast<TutorialCategory>());

            return View(tutorial);
        }

        // POST: Tutorials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TutorialId,TutorialName,TutorialDescription,TutorialVideoUrl,Category")] Tutorial tutorial)
        {
            if (id != tutorial.TutorialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorialExists(tutorial.TutorialId))
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
            return View(tutorial);
        }

        // GET: Tutorials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tutorial == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorial
                .FirstOrDefaultAsync(m => m.TutorialId == id);
            if (tutorial == null)
            {
                return NotFound();
            }

            return View(tutorial);
        }

        // POST: Tutorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tutorial == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tutorial'  is null.");
            }
            var tutorial = await _context.Tutorial.FindAsync(id);
            if (tutorial != null)
            {
                _context.Tutorial.Remove(tutorial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorialExists(int id)
        {
          return (_context.Tutorial?.Any(e => e.TutorialId == id)).GetValueOrDefault();
        }
    }
}
