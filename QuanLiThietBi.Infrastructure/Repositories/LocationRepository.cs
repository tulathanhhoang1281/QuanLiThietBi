using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiThietBi.Infrastructure.Repositories
{
    public class LocationRepository : IRepository<TblLocation>
    {
        private readonly qlthietbiContext _context;

        public LocationRepository(qlthietbiContext context)
        {
            _context = context;
        }

        public void Add(TblLocation entity)
        {
            _context.TblLocations.Add(entity);
        }

        public void Update(TblLocation entity)
        {
            _context.TblLocations.Update(entity);
        }

        public void Delete(TblLocation entity)
        {
            _context.TblLocations.Remove(entity);
        }

        public TblLocation GetByID(int id)
        {
            return _context.TblLocations.Find(id);
        }

        public IEnumerable<TblLocation> GetAll()
        {
            return _context.TblLocations.ToList();
        }

    }
}
