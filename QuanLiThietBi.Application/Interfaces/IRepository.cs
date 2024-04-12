using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiThietBi.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity); 
        void Update(TEntity entity);
        void Delete(int id);
        Task<TEntity> GetByID(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task SaveChangesAsync();
    }
}
