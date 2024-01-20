﻿using Microsoft.EntityFrameworkCore;
using Models;
using Repository.EFInitial;
using Repository.Repos.Interfaces;
using System.Data.Entity.Infrastructure;
using DbUpdateConcurrencyException = Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace Repository.Repos
{
    public class BaseRepository<T> : IDisposable, IRepository<T> where T : BaseModel, new()
    {
        private readonly DbSet<T> _table;
        private readonly DataContext _db;
        protected DataContext Context => _db;

        public BaseRepository() : this(new DataContext())
        {
        }

        public BaseRepository(DataContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public async Task<int> AddAsync(T entity)
        {
            _table.Add(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IList<T> entities)
        {
            await _table.AddRangeAsync(entities);
            return await SaveChangesAsync();
        }

        public async Task<int> SaveAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsStateAsync(Guid id, DateTime createdAt)
        {
            _db.Entry(new T() { Id = id, CreatedAt = createdAt }).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(T entity)
        {
            _db.Remove(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsStateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        //could be problem
        public async Task<T> GetOneAsync(Guid? id) => await _table.FindAsync(id);

        public async Task<int> GetCountAsync() => await _table.CountAsync();

        public virtual async Task<List<T>> GetAllAsync() => await _table.ToListAsync();

        public async Task<List<T>> ExecuteQueryAsync(string sql) => await _table.FromSqlRaw(sql).ToListAsync();

        public async Task<List<T>> ExecuteQueryAsync(string sql, object[] sqlParametersObjects)
            => await _table.FromSqlRaw(sql, sqlParametersObjects).ToListAsync();

        internal async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Thrown when there is a concurrency error
                //for now, just rethrow the exception
                throw ex;
            }
            catch (DbUpdateException ex)
            {
                //Thrown when database update fails
                //Examine the inner exception(s) for additional 
                //details and affected objects
                //for now, just rethrow the exception
                throw ex;
            }
            catch (CommitFailedException ex)
            {
                //handle transaction failures here
                //for now, just rethrow the exception
                throw ex;
            }
            catch (Exception ex)
            {
                //some other exception happened and should be handled
                throw ex;
            }
        }
    }
}
