using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core;
using Dictionary.Core.Models;
using Dictionary.Core.Repositories;
using Dictionary.Data.Repositories;

namespace Dictionary.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MongoDbContext _context;

        private FormsRepository _formsRepository;
        private LemmasRepository _lemmasRepository;

        public UnitOfWork(IDatabaseSettings settings)
        {
            _context = new MongoDbContext(settings);
        }


        public ILemmaRepository<Lemma> Lemmas => _lemmasRepository = _lemmasRepository ?? new LemmasRepository(_context);
        public IFormRepository<Form> Forms => _formsRepository = _formsRepository ?? new FormsRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}