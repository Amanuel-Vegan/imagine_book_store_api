﻿using Imagine_todo.application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Imagine_todo.Persistence.Repositorys
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Exists(Guid id)
        {
            var isExist = await Get(id);
            return isExist != null;
        }

        public async Task<T> Get(Guid id)
        {
            var result = await _dbContext.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

