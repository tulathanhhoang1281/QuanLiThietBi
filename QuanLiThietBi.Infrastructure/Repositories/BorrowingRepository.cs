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
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly qlthietbiContext _dbContext;

        public BorrowingRepository(qlthietbiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TblBorrowing GetByID(int id)
        {
            return _dbContext.TblBorrowings.FirstOrDefault(b => b.BorrowingId == id);
        }

        public IEnumerable<TblBorrowing> GetAll()
        {
            return _dbContext.TblBorrowings.ToList();
        }

        public void AddBorrowing(TblBorrowing borrowing)
        {
            _dbContext.TblBorrowings.Add(borrowing);
            _dbContext.SaveChanges();
        }

        public void UpdateBorrowing(TblBorrowing borrowing)
        {
            _dbContext.TblBorrowings.Update(borrowing);
            _dbContext.SaveChanges();
        }

        public void DeleteBorrowing(int id)
        {
            var borrowing = GetByID(id);
            if (borrowing != null)
            {
                _dbContext.TblBorrowings.Remove(borrowing);
                _dbContext.SaveChanges();
            }
        }
    }
}
