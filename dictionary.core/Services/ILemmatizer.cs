using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dictionary.Core.Services
{
    public interface ILemmatizer
    {
        IEnumerable<string> GetLemmas(string form);
        Task<IEnumerable<string>> GetLemmasAsync(string form);
    }
}