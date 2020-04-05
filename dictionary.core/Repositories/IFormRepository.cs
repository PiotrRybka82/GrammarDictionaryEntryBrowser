using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core.Models;

namespace Dictionary.Core.Repositories
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<Form> GetFormByIdAsync(string id);
    }
}