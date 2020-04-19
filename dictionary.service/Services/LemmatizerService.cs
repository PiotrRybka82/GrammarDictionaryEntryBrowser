using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core;
using Dictionary.Core.Services;

namespace Dictionary.Service.Services
{
    public class LemmatizerService : ILemmatizer
    {
        private readonly IUnitOfWork _unitOfWork;

        public LemmatizerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<string> GetLemmas(string form)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> GetLemmasAsync(string form)
        {
            throw new System.NotImplementedException();
        }
    }
}