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
    public class WinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wines
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Wines.Include(w => w.WineCattegories).Include(w => w.WineTypes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Wines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines
                .Include(w => w.WineCattegories)
                .Include(w => w.WineTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        // GET: Wines/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["WineCattegoriesId"] = new SelectList(_context.WineCattegories, "Id", "Name");
            ViewData["WineTypesId"] = new SelectList(_context.WineTypes, "Id", "Name");
            return View();
        }

        // POST: Wines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Description,ImageUrl,WineCattegoriesId,WineTypesId")] Wine wine)
        {
            wine.DateModified = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Wines.Add(wine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WineCattegoriesId"] = new SelectList(_context.WineCattegories, "Id", "Name", wine.WineCattegoriesId);
            ViewData["WineTypesId"] = new SelectList(_context.WineTypes, "Id", "Name", wine.WineTypesId);
            return View(wine);
        }

        // GET: Wines/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines.FindAsync(id);
            if (wine == null)
            {
                return NotFound();
            }
            ViewData["WineCattegoriesId"] = new SelectList(_context.WineCattegories, "Id", "Name", wine.WineCattegoriesId);
            ViewData["WineTypesId"] = new SelectList(_context.WineTypes, "Id", "Name", wine.WineTypesId);
            return View(wine);
        }

        // POST: Wines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,ImageUrl,WineCattegoriesId,WineTypesId")] Wine wine)
        {
            if (id != wine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    wine.DateModified = DateTime.Now;
                    _context.Wines.Update(wine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WineExists(wine.Id))
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
            ViewData["WineCattegoriesId"] = new SelectList(_context.WineCattegories, "Id", "Name", wine.WineCattegoriesId);
            ViewData["WineTypesId"] = new SelectList(_context.WineTypes, "Id", "Name", wine.WineTypesId);
            return View(wine);
        }

        // GET: Wines/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines
                .Include(w => w.WineCattegories)
                .Include(w => w.WineTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        // POST: Wines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wine = await _context.Wines.FindAsync(id);
            if (wine != null)
            {
                _context.Wines.Remove(wine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineExists(int id)
        {
            return _context.Wines.Any(e => e.Id == id);
        }
    }
}
