using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingWebsite.Storage
{
    public interface IRepository<in TKey, TEntity>
    {
        TEntity Get(TKey id);
        TEntity GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }

    public interface IRepostitoryAsync<in TKey, TEntity>
    {
        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> GetAllAsync();
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
