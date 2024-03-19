using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuanLiThietBi.Domain.Models;
using QuanLiThietBi.Application.Interfaces;
using System.Diagnostics;
using System.Web.Mvc;
namespace QuanLiThietBi.Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var products = _unitOfWork.ProductRepository.GetAll();
            return (IActionResult)View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return (IActionResult)HttpNotFound();
            }
            return (IActionResult)View(product);
        }

        public IActionResult Create()
        {
            return (IActionResult)View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblProduct product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.SaveChanges();
                return (IActionResult)RedirectToAction(nameof(Index));
            }
            return (IActionResult)View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return (IActionResult)HttpNotFound();
            }
            return (IActionResult)View(product);
        }

        public async Task<IActionResult> Edit(int id, TblProduct product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.SaveChanges();
                return (IActionResult)RedirectToAction(nameof(Index));
            }
            return (IActionResult)View(product);
        }

        private IActionResult BadRequest()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return (IActionResult)HttpNotFound();
            }
            return (IActionResult)View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.SaveChanges();
            return (IActionResult)RedirectToAction(nameof(Index));
        }
    }
}
