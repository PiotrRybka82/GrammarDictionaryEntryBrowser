using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core.Models;

namespace Dictionary.Core.Repositories
{
    public interface ILemmaRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Lemma
    {
        Task<IEnumerable<Lemma>> GetAllLemmasAsync();
    }
}