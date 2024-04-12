using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Domain.Models;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Infrastructure;
using QuanLiThietBi.Models;
using qlthietbiContext = QuanLiThietBi.Models.qlthietbiContext;

namespace QuanLiThietBi.Controllers
{
    public class StoresController : Controller
    {
        private readonly qlthietbiContext _context;

        public StoresController(qlthietbiContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
              return _context.TblLocations != null ? 
                          View(await _context.TblLocations.Where(l => l.Type == "Cửa hàng").ToListAsync()) :
                          Problem("Entity set 'qlthietbiContext.TblLocations'  is null.");
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblLocations == null)
            {
                return NotFound();
            }

            var tblLocation = await _context.TblLocations
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (tblLocation == null)
            {
                return NotFound();
            }

            return View(tblLocation);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            TblLocation newLocation = new TblLocation
            {
                Type = "Cửa hàng"
            };
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,NameLocation,Description,Type")] TblLocation tblLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblLocation);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblLocations == null)
            {
                return NotFound();
            }

            var tblLocation = await _context.TblLocations.FindAsync(id);
            if (tblLocation == null)
            {
                return NotFound();
            }
            return View(tblLocation);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationId,NameLocation,Description,Type")] TblLocation tblLocation)
        {
            if (id != tblLocation.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblLocationExists(tblLocation.LocationId))
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
            return View(tblLocation);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblLocations == null)
            {
                return NotFound();
            }

            var tblLocation = await _context.TblLocations
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (tblLocation == null)
            {
                return NotFound();
            }

            return View(tblLocation);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblLocations == null)
            {
                return Problem("Entity set 'qlthietbiContext.TblLocations'  is null.");
            }
            var tblLocation = await _context.TblLocations.FindAsync(id);
            if (tblLocation != null)
            {
                _context.TblLocations.Remove(tblLocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblLocationExists(int id)
        {
          return (_context.TblLocations?.Any(e => e.LocationId == id)).GetValueOrDefault();
        }
    }
}
