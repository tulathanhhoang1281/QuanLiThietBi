﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiThietBi.Application.Interfaces;
namespace QuanLiThietBi.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly qlthietbiContext _context;

        public Repository(qlthietbiContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetByID(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        async Task<TEntity> IRepository<TEntity>.GetByID(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        async Task<IEnumerable<TEntity>> IRepository<TEntity>.GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
    }
}
