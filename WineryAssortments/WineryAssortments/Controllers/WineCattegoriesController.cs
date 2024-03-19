using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WineryAssortments.Data;

namespace WineryAssortments.Controllers
{
    public class WineCattegoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WineCattegoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WineCattegories
        public async Task<IActionResult> Index()
        {
            return View(await _context.WineCattegories.ToListAsync());
        }

        // GET: WineCattegories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineCattegory = await _context.WineCattegories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wineCattegory == null)
            {
                return NotFound();
            }

            return View(wineCattegory);
        }

        // GET: WineCattegories/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: WineCattegories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] WineCattegory wineCattegory)
        {
            wineCattegory.DateModified = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(wineCattegory);
                
            }
            _context.WineCattegories.Add(wineCattegory);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
        }

        // GET: WineCattegories/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineCattegory = await _context.WineCattegories.FindAsync(id);
            if (wineCattegory == null)
            {
                return NotFound();
            }
            return View(wineCattegory);
        }

        // POST: WineCattegories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] WineCattegory wineCattegory)
        {
            if (id != wineCattegory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    wineCattegory.DateModified = DateTime.Now;
                    _context.WineCattegories.Update(wineCattegory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WineCattegoryExists(wineCattegory.Id))
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
            return View(wineCattegory);
        }

        // GET: WineCattegories/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineCattegory = await _context.WineCattegories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wineCattegory == null)
            {
                return NotFound();
            }

            return View(wineCattegory);
        }

        // POST: WineCattegories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wineCattegory = await _context.WineCattegories.FindAsync(id);
            if (wineCattegory != null)
            {
                _context.WineCattegories.Remove(wineCattegory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineCattegoryExists(int id)
        {
            return _context.WineCattegories.Any(e => e.Id == id);
        }
    }
}
