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
    public class WineTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WineTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WineTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WineTypes.ToListAsync());
        }

        // GET: WineTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineType = await _context.WineTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wineType == null)
            {
                return NotFound();
            }

            return View(wineType);
        }

        // GET: WineTypes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: WineTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] WineType wineType)
        {
            wineType.DateModified = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(wineType);
            }
            _context.WineTypes.Add(wineType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: WineTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineType = await _context.WineTypes.FindAsync(id);
            if (wineType == null)
            {
                return NotFound();
            }
            return View(wineType);
        }

        // POST: WineTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] WineType wineType)
        {
            if (id != wineType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    wineType.DateModified = DateTime.Now;
                    _context.WineTypes.Update(wineType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WineTypeExists(wineType.Id))
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
            return View(wineType);
        }

        // GET: WineTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineType = await _context.WineTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wineType == null)
            {
                return NotFound();
            }

            return View(wineType);
        }

        // POST: WineTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wineType = await _context.WineTypes.FindAsync(id);
            if (wineType != null)
            {
                _context.WineTypes.Remove(wineType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineTypeExists(int id)
        {
            return _context.WineTypes.Any(e => e.Id == id);
        }
    }
}
