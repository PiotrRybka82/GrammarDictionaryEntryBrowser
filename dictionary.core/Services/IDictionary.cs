using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core.Models;

namespace Dictionary.Core.Services
{
    public interface IDictionary
    {
        IEnumerable<Entry> GetEntries(string form);
        Task<IEnumerable<Entry>> GetEntriesAsync(string form);
    }
}