using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiThietBi.Domain.Models;
namespace QuanLiThietBi.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TblCategory> CategoryRepository { get; }
        IRepository<TblProduct> ProductRepository { get; }
        int SaveChanges();
    }
}
