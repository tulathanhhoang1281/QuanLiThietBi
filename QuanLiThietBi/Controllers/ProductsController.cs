using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Domain.Models;
using QuanLiThietBi.Infrastructure.UnitOfWork;
using QuanLiThietBi.Models;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Rendering;

namespace QuanLiThietBi.Controllers
{
    public class ProductsController : Controller
    {
        private readonly qlthietbiContext _context;
        private readonly IRepository<TblProduct> _productRepository;

        public ProductsController(qlthietbiContext context, IRepository<TblProduct> productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        //private string GenerateBarcode(string serialNumber)
        //{
        //    var barcode = BarcodeWriter.CreateBarcode(serialNumber, BarcodeWriterEncoding.Code128);
        //    var barcodeImage = barcode.RenderImage();

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //        byte[] imageBytes = ms.ToArray();
        //        string base64Image = Convert.ToBase64String(imageBytes);
        //        return "data:image/png;base64," + base64Image;
        //    }
        //}

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //var qlthietbiContext = _context.TblProducts.Include(t => t.Category).Include(t => t.Location);
            //return View(await qlthietbiContext.ToListAsync());
            var products = await _productRepository.GetAll();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblProducts == null)
            {
                return NotFound();
            }
            var tblProduct = await _context.TblProducts
                .Include(t => t.Category)
                .Include(t => t.Location)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tblProduct == null)
            {
                return NotFound();
            }
            return View(tblProduct);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.TblCategories, "CategoryId", "Description");
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Description");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId,Name,Model,Manufacturer,SerialNumber,PurchaseDate,WarrantyEndDate,Status,LocationId")] TblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.TblCategories, "CategoryId", "Description", tblProduct.CategoryId);
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Description", tblProduct.LocationId);
            return View(tblProduct);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblProducts == null)
            {
                return NotFound();
            }

            var tblProduct = await _context.TblProducts.FindAsync(id);
            if (tblProduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.TblCategories, "CategoryId", "Description", tblProduct.CategoryId);
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Description", tblProduct.LocationId);
            return View(tblProduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,Name,Model,Manufacturer,SerialNumber,PurchaseDate,WarrantyEndDate,Status,LocationId")] TblProduct tblProduct)
        {
            if (id != tblProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblProductExists(tblProduct.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.TblCategories, "CategoryId", "Description", tblProduct.CategoryId);
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Description", tblProduct.LocationId);
            return View(tblProduct);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblProducts == null)
            {
                return NotFound();
            }

            var tblProduct = await _context.TblProducts
                .Include(t => t.Category)
                .Include(t => t.Location)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tblProduct == null)
            {
                return NotFound();
            }

            return View(tblProduct);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblProducts == null)
            {
                return Problem("Entity set 'qlthietbiContext.TblProducts'  is null.");
            }
            var tblProduct = await _context.TblProducts.FindAsync(id);
            if (tblProduct != null)
            {
                _context.TblProducts.Remove(tblProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblProductExists(int id)
        {
          return (_context.TblProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
