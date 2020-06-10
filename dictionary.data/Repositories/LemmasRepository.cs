using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core.Models;
using Dictionary.Core.Repositories;
using MongoDB.Driver;

namespace Dictionary.Data.Repositories
{
    public class LemmasRepository : BaseRepository<Lemma>, ILemmaRepository<Lemma>
    {
        public LemmasRepository(MongoDbContext context) : base(context, context.Lemmas) { }

        public async Task<IEnumerable<Lemma>> GetAllLemmasAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }
    }
}