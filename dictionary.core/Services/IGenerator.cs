using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dictionary.Core.Services
{
    public interface IGenerator
    {
        IEnumerable<string> GetForms(string lemma, IEnumerable<string> categories);
        Task<IEnumerable<string>> GetFormsAsync(string lemma, IEnumerable<string> categories);
    }
}