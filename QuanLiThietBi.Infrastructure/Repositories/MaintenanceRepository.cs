using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiThietBi.Infrastructure.Repositories
{
    public class MaintenanceRepository : IRepository<TblMaintenance>
    {
        private readonly qlthietbiContext _context;

        public MaintenanceRepository(qlthietbiContext context)
        {
            _context = context;
        }

        public void Add(TblMaintenance entity)
        {
            _context.TblMaintenances.Add(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(TblMaintenance entity)
        {
            _context.TblMaintenances.Remove(entity);
            _context.SaveChangesAsync();
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

        public void Update(TblMaintenance entity)
        {
            _context.TblMaintenances.Update(entity);
        }
    }
}
