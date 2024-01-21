using System;
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
    public class PhieuNHsController : Controller
    {
        private readonly DemoContext _context;

        public PhieuNHsController(DemoContext context)
        {
            _context = context;
        }

        // GET: PhieuNHs
        public async Task<IActionResult> Index()
        {
            var demoContext = _context.PhieuNH.Include(p => p.User);
            return View(await demoContext.ToListAsync());
        }

        // GET: PhieuNHs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuNH = await _context.PhieuNH
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuNH == null)
            {
                return NotFound();
            }

            return View(phieuNH);
        }

        // GET: PhieuNHs/Create
        public IActionResult Create()
        {
            ViewData["userName"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }
        // POST: PhieuNHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Ngaynhap,Soluong,Dongia,userName,TongTien")] PhieuNH phieuNH)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieuNH);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["userName"] = new SelectList(_context.User, "Id", "Id", phieuNH.userName);
            return View(phieuNH);
        }

        // GET: PhieuNHs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuNH = await _context.PhieuNH.FindAsync(id);
            if (phieuNH == null)
            {
                return NotFound();
            }
            ViewData["userName"] = new SelectList(_context.User, "Id", "Id", phieuNH.userName);
            return View(phieuNH);
        }

        // POST: PhieuNHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ngaynhap,Soluong,Dongia,userName,TongTien")] PhieuNH phieuNH)
        {
            if (id != phieuNH.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuNH);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuNHExists(phieuNH.Id))
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
            ViewData["userName"] = new SelectList(_context.User, "Id", "Id", phieuNH.userName);
            return View(phieuNH);
        }

        // GET: PhieuNHs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuNH = await _context.PhieuNH
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuNH == null)
            {
                return NotFound();
            }

            return View(phieuNH);
        }

        // POST: PhieuNHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phieuNH = await _context.PhieuNH.FindAsync(id);
            if (phieuNH != null)
            {
                _context.PhieuNH.Remove(phieuNH);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuNHExists(int id)
        {
            return _context.PhieuNH.Any(e => e.Id == id);
        }
    }
}
