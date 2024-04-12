using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Domain.Models;
using QuanLiThietBi.Infrastructure.Repositories;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Infrastructure;
namespace QuanLiThietBi.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly qlthietbiContext _context;
        private readonly IRepository<TblCategory> _categoryRepository;
        private readonly IRepository<TblProduct> _productRepository;
        private readonly IRepository<TblComponent> _componentRepository;
        private readonly IRepository<TblUser> _userRepository;
        private readonly IRepository<TblRole> _roleRepository;
        private readonly IRepository<TblOrder> _orderRepository;
        private readonly IRepository<TblLocation> _locationRepository;
        private readonly IRepository<TblEmployee> _employeeRepository;
        private readonly IRepository<TblBorrowing> _borrowingRepository;
        private readonly IRepository<TblMaintenance> _maintenanceRepository;
        public UnitOfWork(qlthietbiContext context)
        {
            _context = context;
            _categoryRepository = new Repository<TblCategory>(_context);
            _productRepository = new Repository<TblProduct>(_context);
            _componentRepository =new Repository<TblComponent>(_context);
            _userRepository = new Repository<TblUser>(_context);
            _roleRepository = new Repository<TblRole>(_context);
            _orderRepository = new Repository<TblOrder>(_context);
            _locationRepository = new Repository<TblLocation>(_context);
            _employeeRepository = new Repository<TblEmployee>(_context);
            _borrowingRepository = new Repository<TblBorrowing>(_context);
            _maintenanceRepository = new Repository<TblMaintenance>(_context);
        }

        public IRepository<TblCategory> CategoryRepository => _categoryRepository;

        public IRepository<TblProduct> ProductRepository => _productRepository;
        
        public IRepository<TblComponent> ComponentRepository => _componentRepository;

        public IRepository<TblUser> UserRepository => _userRepository;

        public IRepository<TblRole> RoleRepository => _roleRepository;

        public IRepository<TblOrder> OrderRepository => _orderRepository;

        public IRepository<TblLocation> LocationRepository => _locationRepository;

        public IRepository<TblEmployee> EmployeeRepository => _employeeRepository;

        public IRepository<TblBorrowing> BorrowingRepository => _borrowingRepository;

        public IRepository<TblMaintenance> MaintenanceRepository => _maintenanceRepository;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
