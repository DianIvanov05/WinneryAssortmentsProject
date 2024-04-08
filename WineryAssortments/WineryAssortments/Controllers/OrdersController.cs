using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WineryAssortments.Data;

namespace WineryAssortments.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var applicationDbConxtext = _context.Orders
                                    .Include(o => o.Customers)
                                    .Include(o => o.Wines);
                return View(await applicationDbConxtext.ToListAsync());
            }
            else
            {
                var applicationDbConxtext = _context.Orders
                                    .Include(o => o.Customers)
                                    .Include(o => o.Wines)
                                    .Where(x => x.CustomersId == _userManager.GetUserId(User));
                return View(await applicationDbConxtext.ToListAsync());
            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customers)
                .Include(o => o.Wines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            //ViewData["CustomersId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["WinesId"] = new SelectList(_context.Wines, "Id", "Name");
            return View();
        }
      
        public async Task<IActionResult> CreateWithWineId(int wineId, int countP)
        {
            var currentWine = await _context.Wines.FirstOrDefaultAsync(z => z.Id == wineId);
            Order order = new Order();
            //order.ProductsId = productId;
            // productId = order.ProductsId;
            order.WinesId = wineId;
            order.Quantity = countP;
            order.CustomersId = _userManager.GetUserId(User);
            var price = countP * currentWine.Price;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WinesId,Quantity")] Order order)
        {
            order.DateModified = DateTime.Now;
            order.CustomersId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CustomersId"] = new SelectList(_context.Users, "Id", "Id", order.CustomersId);
            ViewData["WinesId"] = new SelectList(_context.Wines, "Id", "Name", order.WinesId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            //ViewData["CustomersId"] = new SelectList(_context.Users, "Id", "Id", order.CustomersId);
            ViewData["WinesId"] = new SelectList(_context.Wines, "Id", "Id", order.WinesId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WinesId,Quantity,DateModified")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.CustomersId = _userManager.GetUserId(User);
                    order.DateModified = DateTime.Now;
                    _context.Orders.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            //ViewData["CustomersId"] = new SelectList(_context.Users, "Id", "Id", order.CustomersId);
            ViewData["WinesId"] = new SelectList(_context.Wines, "Id", "Id", order.WinesId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customers)
                .Include(o => o.Wines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
