using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core.Models;

namespace Dictionary.Core.Repositories
{
    public interface ILemmaRepository : IRepository<Lemma>
    {
        Task<IEnumerable<Lemma>> GetAllLemmasAsync();

        Task<Lemma> GetLemmaByIdAsync(string id);
    }
}