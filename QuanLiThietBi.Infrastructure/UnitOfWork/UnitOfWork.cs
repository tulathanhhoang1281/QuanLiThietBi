using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Domain.Models;
using QuanLiThietBi.Infrastructure.Repositories;
using QuanLiThietBi.Application.Interfaces;
namespace QuanLiThietBi.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly qlthietbiContext _context;
        private readonly IRepository<TblCategory> _categoryRepository;
        private readonly IRepository<TblProduct> _productRepository;

        public UnitOfWork(qlthietbiContext context)
        {
            _context = context;
            _categoryRepository = new Repository<TblCategory>(_context);
            _productRepository = new Repository<TblProduct>(_context);
        }

        public IRepository<TblCategory> CategoryRepository => _categoryRepository;

        public IRepository<TblProduct> ProductRepository => _productRepository;

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
