using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core;
using Dictionary.Core.Models;
using Dictionary.Core.Services;

namespace Dictionary.Service.Services
{
    public class DictionaryService : IDictionary
    {
        private readonly IUnitOfWork _unitOfWork;

        public DictionaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Entry> GetEntries(string form)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Entry> GetEntries(string form, IEnumerable<string> categories = null, bool useRegEx = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Entry>> GetEntriesAsync(string form)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Entry>> GetEntriesAsync(string form, IEnumerable<string> categories = null, bool useRegEx = false)
        {
            throw new System.NotImplementedException();
        }
    }
}