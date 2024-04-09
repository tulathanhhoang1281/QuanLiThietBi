using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiThietBi.Domain.Models;
namespace QuanLiThietBi.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TblCategory> CategoryRepository { get; }
        IRepository<TblProduct> ProductRepository { get; }
        IRepository<TblComponent> ComponentRepository { get; }
        IRepository<TblUser> UserRepository { get; }
        IRepository<TblRole> RoleRepository { get; }
        IRepository<TblOrder> OrderRepository { get; }
        IRepository<TblLocation> LocationRepository { get; }
        IRepository<TblEmployee> EmployeeRepository { get; }
        IRepository<TblBorrowing> BorrowingRepository { get; }
        IRepository<TblMaintenance> MaintenanceRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
