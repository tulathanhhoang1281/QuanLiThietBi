using QuanLiThietBi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiThietBi.Application.Interfaces
{
    public interface ICategoriesRepository : IRepository<TblCategory>
    {
        TblCategory GetCategoryById(int id);
    }
}
