using QuanLiThietBi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiThietBi.Application.Interfaces
{
    public interface IBorrowingRepository
    {
        TblBorrowing GetByID(int id);
        IEnumerable<TblBorrowing> GetAll();
        void AddBorrowing(TblBorrowing borrowing);
        void UpdateBorrowing(TblBorrowing borrowing);
        void DeleteBorrowing(int id); 
    }
}
