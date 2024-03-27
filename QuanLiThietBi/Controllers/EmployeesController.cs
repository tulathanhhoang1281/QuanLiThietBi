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
    public class EmployeesController : Controller
    {
        private readonly qlthietbiContext _context;

        public EmployeesController(qlthietbiContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var qlthietbiContext = _context.TblEmployees.Include(t => t.Location).Include(t => t.Role);
            return View(await qlthietbiContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblEmployees == null)
            {
                return NotFound();
            }

            var tblEmployee = await _context.TblEmployees
                .Include(t => t.Location)
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (tblEmployee == null)
            {
                return NotFound();
            }

            return View(tblEmployee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Description");
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "Description");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,Address,PhoneNumber,Email,LocationId,RoleId")] TblEmployee tblEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Description", tblEmployee.LocationId);
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "Description", tblEmployee.RoleId);
            return View(tblEmployee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblEmployees == null)
            {
                return NotFound();
            }

            var tblEmployee = await _context.TblEmployees.FindAsync(id);
            if (tblEmployee == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Description", tblEmployee.LocationId);
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "Description", tblEmployee.RoleId);
            return View(tblEmployee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,Address,PhoneNumber,Email,LocationId,RoleId")] TblEmployee tblEmployee)
        {
            if (id != tblEmployee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblEmployeeExists(tblEmployee.EmployeeId))
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
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Description", tblEmployee.LocationId);
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "Description", tblEmployee.RoleId);
            return View(tblEmployee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblEmployees == null)
            {
                return NotFound();
            }

            var tblEmployee = await _context.TblEmployees
                .Include(t => t.Location)
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (tblEmployee == null)
            {
                return NotFound();
            }

            return View(tblEmployee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblEmployees == null)
            {
                return Problem("Entity set 'qlthietbiContext.TblEmployees'  is null.");
            }
            var tblEmployee = await _context.TblEmployees.FindAsync(id);
            if (tblEmployee != null)
            {
                _context.TblEmployees.Remove(tblEmployee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblEmployeeExists(int id)
        {
          return (_context.TblEmployees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
