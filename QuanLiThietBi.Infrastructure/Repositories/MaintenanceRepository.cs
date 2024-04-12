using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   
//using QuanLiThietBi.Models;

namespace QuanLiThietBi.Infrastructure.Repositories
{
    public class MaintenanceRepository : IRepository<TblMaintenance>
    {
        private readonly qlthietbiContext _context;

        public MaintenanceRepository(qlthietbiContext context)
        {
            _context = context;
        }

        public async void Add(TblMaintenance entity)
        {
            await _context.TblMaintenances.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async void Delete(int id)
        {
            var entity = await _context.TblMaintenances.FindAsync(id);
            _context.TblMaintenances.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<TblMaintenance>> GetAll()
        {
            return await _context.TblMaintenances.ToListAsync();
        }

        public async Task<TblMaintenance> GetByID(int id)
        {
            return await _context.TblMaintenances.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(TblMaintenance entity)
        {
            _context.TblMaintenances.Update(entity);
        }

    }
}
