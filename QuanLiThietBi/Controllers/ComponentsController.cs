using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Domain.Models;
using QuanLiThietBi.Models;

namespace QuanLiThietBi.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly qlthietbiContext _context;

        public ComponentsController(qlthietbiContext context)
        {
            _context = context;
        }

        // GET: Components
        public async Task<IActionResult> Index()
        {
            var qlthietbiContext = _context.TblComponents.Include(t => t.Product);
            return View(await qlthietbiContext.ToListAsync());
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblComponents == null)
            {
                return NotFound();
            }

            var tblComponent = await _context.TblComponents
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (tblComponent == null)
            {
                return NotFound();
            }

            return View(tblComponent);
        }

        // GET: Components/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "Manufacturer");
            return View();
        }

        // POST: Components/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,ProductId,Name,Manufacturer,SerialNumber,Dvt")] TblComponent tblComponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "Manufacturer", tblComponent.ProductId);
            return View(tblComponent);
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblComponents == null)
            {
                return NotFound();
            }

            var tblComponent = await _context.TblComponents.FindAsync(id);
            if (tblComponent == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "Manufacturer", tblComponent.ProductId);
            return View(tblComponent);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComponentId,ProductId,Name,Manufacturer,SerialNumber,Dvt")] TblComponent tblComponent)
        {
            if (id != tblComponent.ComponentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblComponentExists(tblComponent.ComponentId))
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
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "Manufacturer", tblComponent.ProductId);
            return View(tblComponent);
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblComponents == null)
            {
                return NotFound();
            }

            var tblComponent = await _context.TblComponents
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (tblComponent == null)
            {
                return NotFound();
            }

            return View(tblComponent);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblComponents == null)
            {
                return Problem("Entity set 'qlthietbiContext.TblComponents'  is null.");
            }
            var tblComponent = await _context.TblComponents.FindAsync(id);
            if (tblComponent != null)
            {
                _context.TblComponents.Remove(tblComponent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblComponentExists(int id)
        {
          return (_context.TblComponents?.Any(e => e.ComponentId == id)).GetValueOrDefault();
        }
    }
}
