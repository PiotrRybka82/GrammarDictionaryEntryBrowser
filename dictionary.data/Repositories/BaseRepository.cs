using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dictionary.Core.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Dictionary.Data.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : Base
    {
        protected readonly MongoDbContext _context;
        protected readonly IMongoCollection<TEntity> _collection;

        public BaseRepository(MongoDbContext context, IMongoCollection<TEntity> collection)
        {
            _context = context;
            _collection = collection;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _collection.AsQueryable().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _collection.AsQueryable().FirstOrDefaultAsync(x => ((string)x.Id).Equals((string)id));
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _collection.AsQueryable().Where(predicate).ToListAsync();
        }



    }
}