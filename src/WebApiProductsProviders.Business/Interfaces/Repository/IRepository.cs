using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Business.Interfaces.Repository
{

    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task Insert(TEntity entity);
        Task<TEntity> FindById(Guid id);
        Task<List<TEntity>> FindAll();
        Task Update(TEntity entity);
        Task Remove(Guid id);
        Task<List<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
