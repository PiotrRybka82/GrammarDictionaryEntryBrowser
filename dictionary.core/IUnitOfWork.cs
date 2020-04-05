using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core.Models;
using Dictionary.Core.Repositories;

namespace Dictionary.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ILemmaRepository<Lemma> Lemmas { get; }
        IFormRepository<Form> Forms { get; }

        Task<int> CommitAsync();
    }
}