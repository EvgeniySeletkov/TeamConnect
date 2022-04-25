using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamConnect.Models;

namespace TeamConnect.Services.Repository
{
    public interface IRepository
    {
        Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new();

        Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null) where T : IEntityBase, new();

        Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new();

        Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new();

        Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate = null) where T : IEntityBase, new();
    }
}
