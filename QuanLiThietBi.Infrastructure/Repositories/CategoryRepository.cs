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
    public class CategoryRepository : IRepository<TblCategory>
    {
        private readonly qlthietbiContext _context;

        public CategoryRepository(qlthietbiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblCategory>> GetAll()
        {
            return await _context.TblCategories.ToListAsync();
        }

        public async Task<TblCategory> GetByID(int id)
        {
            return await _context.TblCategories.FindAsync(id);
        }

        public async void Add(TblCategory entity)
        {
            _context.TblCategories.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async void Update(TblCategory entity)
        {
            _context.TblCategories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async void Delete(int id)
        {
            var entity = await _context.TblCategories.FindAsync(id);
            _context.TblCategories.Remove(entity);
            await SaveChangesAsync();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
