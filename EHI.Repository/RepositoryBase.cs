using EHI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EHI.Repository
{
    public abstract class RepositoryBase<T, TContext> : IRepositoryBase<T> where T : class where TContext : DbContext
    {
        private readonly TContext context;
        public RepositoryBase(TContext context)
        {
            this.context = context;
        }

        public Task<T> Create(T entity)
        {
            this.context.Set<T>().AddAsync(entity);
            this.context.SaveChangesAsync();
            return Task.FromResult(entity);
        }

        public Task<T> Delete(T entity)
        {
            this.context.Set<T>().Remove(entity);
            this.context.SaveChangesAsync();
            return Task.FromResult(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this.context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.context.Set<T>().Where(expression).AsNoTracking();
        }

        public Task<T> Update(T entity)
        {
            this.context.Set<T>().Update(entity);
            this.context.SaveChangesAsync();
            return Task.FromResult(entity);
        }
    }
}
