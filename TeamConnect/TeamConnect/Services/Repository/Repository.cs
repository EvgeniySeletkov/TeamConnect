using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;
using TeamConnect.Models;
using TeamConnect.Models.Leave;
using TeamConnect.Models.Team;
using TeamConnect.Models.User;

namespace TeamConnect.Services.Repository
{
    public class Repository : IRepository
    {
        private Lazy<SQLiteAsyncConnection> _database;

        public Repository()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.DATABASE_NAME);
                var database = new SQLiteAsyncConnection(path);

                database.CreateTableAsync<UserModel>().Wait();
                database.CreateTableAsync<TeamModel>().Wait();
                database.CreateTableAsync<LeaveModel>().Wait();

                return database;
            });
        }

        public Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.DeleteAsync(entity);
        }

        public Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null) where T : IEntityBase, new()
        {
            return predicate is null
                ? _database.Value.Table<T>().ToListAsync()
                : _database.Value.Table<T>().Where(predicate).ToListAsync();
        }

        public Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.InsertAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.UpdateAsync(entity);
        }

        public Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate = null) where T : IEntityBase, new()
        {
            return _database.Value.FindAsync(predicate);
        }
    }
}
