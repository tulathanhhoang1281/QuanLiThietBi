using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiThietBi.Infrastructure;
namespace QuanLiThietBi.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<TblProduct>
    {
        private readonly qlthietbiContext _context;

        public ProductRepository(qlthietbiContext context)
        {
            _context = context;
        }
        public async void Add(TblProduct entity)
        {
            _context.TblProducts.Add(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(TblProduct entity)
        {
            _context.TblProducts.Remove(entity);
        }

        public async void Delete(int id)
        {
            var entity = await _context.TblProducts.FindAsync(id);
            _context.TblProducts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TblProduct>> GetAll()
        {
            return await _context.TblProducts.ToListAsync();
        }

        public async Task<TblProduct> GetByID(int id)
        {
            return await _context.TblProducts.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(TblProduct entity)
        {
            _context.TblProducts.Update(entity);
        }
    }
}
