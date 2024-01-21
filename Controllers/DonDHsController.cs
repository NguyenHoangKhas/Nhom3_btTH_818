
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo.Data;
using Demo.Models;

namespace Demo.Controllers
{
    public class DonDHsController : Controller
    {
        private readonly DemoContext _context;

        public DonDHsController(DemoContext context)
        {
            _context = context;
        }

        // GET: DonDHs
        public async Task<IActionResult> Index()
        {
            var demoContext = _context.DonDH.Include(d => d.Product);
            return View(await demoContext.ToListAsync());
        }

        // GET: DonDHs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donDH = await _context.DonDH
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donDH == null)
            {
                return NotFound();
            }

            return View(donDH);
        }

        // GET: DonDHs/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            return View();
        }

        public IActionResult Count()
        {
            var DonDHS = _context.DonDH;
            var productCount = from product in DonDHS
                               group product by new { product.ProductId } into g
                               select new {  Namea = g.Key.ProductId, Totalquantity = g.Sum(d => d.Quantity) };

            return View(productCount);
        }
        public IActionResult CalculateRevenueByDay()
        {
            var orders = _context.DonDH;

            var revenueByDay = from order in orders
                               group order by new { order.Datetime.Date} into g
                               select new { Date = g.Key, TotalRevenue = g.Sum(order => order.DonGia * order.Quantity) };

            return View(revenueByDay);
        }
        public IActionResult CalculateRevenueByMonth()
        {
            var orders = _context.DonDH;

            var revenueByMonth = from order in orders
                               group order by new { order.Datetime.Month } into g
                               select new { Month = g.Key, TotalRevenueMonth = g.Sum(order => order.DonGia * order.Quantity) };

            return View(revenueByMonth);
        }
        // POST: DonDHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,sdt,Datetime,DonGia,Quantity,ProductId")] DonDH donDH)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donDH);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", donDH.ProductId);
            return View(donDH);
        }

        // GET: DonDHs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donDH = await _context.DonDH.FindAsync(id);
            if (donDH == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", donDH.ProductId);
            return View(donDH);
        }

        // POST: DonDHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,sdt,Datetime,DonGia,Quantity,ProductId")] DonDH donDH)
        {
            if (id != donDH.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donDH);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonDHExists(donDH.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", donDH.ProductId);
            return View(donDH);
        }

        // GET: DonDHs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donDH = await _context.DonDH
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donDH == null)
            {
                return NotFound();
            }

            return View(donDH);
        }

        // POST: DonDHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donDH = await _context.DonDH.FindAsync(id);
            if (donDH != null)
            {
                _context.DonDH.Remove(donDH);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonDHExists(int id)
        {
            return _context.DonDH.Any(e => e.Id == id);
        }
    }
}
