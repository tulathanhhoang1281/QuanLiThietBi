using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiThietBi.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<TblProduct>
    {
        private readonly qlthietbiContext _context;

        public ProductRepository(qlthietbiContext context)
        {
            _context = context;
        }
        public void Add(TblProduct entity)
        {
            _context.TblProducts.Add(entity);
        }

        public void Delete(TblProduct entity)
        {
            _context.TblProducts.Remove(entity);
        }

        public async Task<IEnumerable<TblProduct>> GetAll()
        {
            return await _context.TblProducts.ToListAsync();
        }

        public async Task<TblProduct> GetByID(int id)
        {
            return await _context.TblProducts.FindAsync(id);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TblProduct entity)
        {
            throw new NotImplementedException();
        }
    }
}
