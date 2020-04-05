using Dictionary.Core.Models;
using Dictionary.Core.Repositories;

namespace Dictionary.Data.Repositories
{
    public class FormsRepository : BaseRepository<Form>, IFormRepository<Form>
    {
        public FormsRepository(MongoDbContext context) : base(context, context.Forms) { }
    }
}