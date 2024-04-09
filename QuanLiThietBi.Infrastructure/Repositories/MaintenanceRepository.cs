using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Models;
using System;
using System.Collections.Generic;
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
            _context.TblMain.Add(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(TblMaintenance entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TblMaintenance>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TblMaintenance> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TblMaintenance entity)
        {
            throw new NotImplementedException();
        }
    }
}
