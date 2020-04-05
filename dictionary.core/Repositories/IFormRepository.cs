using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core.Models;

namespace Dictionary.Core.Repositories
{
    public interface IFormRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Form
    {

    }
}