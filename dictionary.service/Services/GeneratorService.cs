using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core;
using Dictionary.Core.Services;

namespace Dictionary.Service.Services
{
    public class GeneratorService : IGenerator
    {
        private readonly IUnitOfWork _unitOfWork;

        public GeneratorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<string> GetForms(string lemma, IEnumerable<string> categories)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> GetFormsAsync(string lemma, IEnumerable<string> categories)
        {
            throw new System.NotImplementedException();
        }




    }
}