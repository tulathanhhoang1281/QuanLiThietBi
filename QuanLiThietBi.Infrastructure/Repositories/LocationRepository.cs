using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using QuanLiThietBi.Models;
namespace QuanLiThietBi.Infrastructure.Repositories
{
    public class LocationRepository : IRepository<TblLocation>
    {
        private readonly qlthietbiContext _context;

        public LocationRepository(qlthietbiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblLocation>> GetAll()
        {
            return await _context.TblLocations.ToListAsync();
        }

        public async Task<TblLocation> GetByID(int id)
        {
            return await _context.TblLocations.FindAsync(id);
        }

        public async void Add(TblLocation entity)
        {
            _context.TblLocations.Add(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(TblLocation entity)
        {
            _context.TblLocations.Update(entity);
        }

        public async void Delete(int id)
        {
            var entity = await _context.TblLocations.FindAsync(id);
            _context.TblLocations.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
