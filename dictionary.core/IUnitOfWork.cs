using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core.Repositories;

namespace Dictionary.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ILemmaRepository Lemmas { get; }
        IFormRepository Forms { get; }

        Task<int> CommitAsync();
    }
}