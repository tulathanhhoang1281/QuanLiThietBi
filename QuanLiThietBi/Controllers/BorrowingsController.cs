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
    public class BorrowingsController : Controller
    {
        private readonly qlthietbiContext _context;

        public BorrowingsController(qlthietbiContext context)
        {
            _context = context;
        }

        // GET: Borrowings
        public async Task<IActionResult> Index()
        {
            var qlthietbiContext = _context.TblBorrowings.Include(t => t.Product).Include(t => t.User);
            return View(await qlthietbiContext.ToListAsync());
        }

        public IActionResult History(int id)
        {
            var borrowings = _context.TblBorrowings.Where(b => b.UserId == id).Include(b => b.Product).ToList();
            return View(borrowings);
        }
        // GET: Borrowings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblBorrowings == null)
            {
                return NotFound();
            }

            var tblBorrowing = await _context.TblBorrowings
                .Include(t => t.Product)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.BorrowingId == id);
            if (tblBorrowing == null)
            {
                return NotFound();
            }

            return View(tblBorrowing);
        }

        // GET: Borrowings/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "Name");
            ViewData["UserId"] = new SelectList(_context.TblUsers, "UserId", "Username");
            return View();
        }

        // POST: Borrowings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowingId,UserId,ProductId,BorrowDate,ReturnDate,Status")] TblBorrowing tblBorrowing)
        {
            if (ModelState.IsValid)
            {
                if (tblBorrowing.BorrowDate <= DateTime.Now) // Validate borrow date
                {
                    tblBorrowing.Status = 1;
                    _context.Add(tblBorrowing);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("BorrowDate", "Borrow date cannot be in the future.");
                }
            }
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "Name", tblBorrowing.ProductId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "UserId", "Username", tblBorrowing.UserId);
            return View(tblBorrowing);
        }

        public IActionResult Approve(int? id)
        {
            if (id == null || _context.TblBorrowings == null)
            {
                return NotFound();
            }
            var borrowings = _context.TblBorrowings.Find(id);
            if(borrowings != null)
            {
                borrowings.Status = 2; // Đã mượn
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int? id)
        {
            if (id == null || _context.TblBorrowings == null)
            {
                return NotFound();
            }
            var borrowing = _context.TblBorrowings.Find(id);
            if (borrowing != null)
            {
                _context.TblBorrowings.Remove(borrowing); //Đã từ chối
                borrowing.Status = 3;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Return(int? id)
        {
            if (id == null || _context.TblBorrowings == null)
            {
                return NotFound();
            }
            var borrowing = _context.TblBorrowings.Find(id);
            if (borrowing != null)
            {
                borrowing.Status = 2; // Đã trả về
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Borrowings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblBorrowings == null)
            {
                return NotFound();
            }

            var tblBorrowing = await _context.TblBorrowings.FindAsync(id);
            if (tblBorrowing == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "Manufacturer", tblBorrowing.ProductId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "UserId", "Password", tblBorrowing.UserId);
            return View(tblBorrowing);
        }

        // POST: Borrowings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowingId,UserId,ProductId,BorrowDate,ReturnDate,Status")] TblBorrowing tblBorrowing)
        {
            if (id != tblBorrowing.BorrowingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblBorrowing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblBorrowingExists(tblBorrowing.BorrowingId))
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
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "Manufacturer", tblBorrowing.ProductId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "UserId", "Password", tblBorrowing.UserId);
            return View(tblBorrowing);
        }

        // GET: Borrowings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblBorrowings == null)
            {
                return NotFound();
            }

            var tblBorrowing = await _context.TblBorrowings
                .Include(t => t.Product)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.BorrowingId == id);
            if (tblBorrowing == null)
            {
                return NotFound();
            }

            return View(tblBorrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblBorrowings == null)
            {
                return Problem("Entity set 'qlthietbiContext.TblBorrowings'  is null.");
            }
            var tblBorrowing = await _context.TblBorrowings.FindAsync(id);
            if (tblBorrowing != null)
            {
                _context.TblBorrowings.Remove(tblBorrowing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblBorrowingExists(int id)
        {
          return (_context.TblBorrowings?.Any(e => e.BorrowingId == id)).GetValueOrDefault();
        }
    }
}
