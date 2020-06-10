using System;
using System.Threading.Tasks;
using Dictionary.Core;
using Dictionary.Core.Models;
using MongoDB.Driver;

namespace Dictionary.Data
{
    public class MongoDbContext : IDisposable
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;

        private readonly IMongoCollection<Lemma> _lemmas;
        private readonly IMongoCollection<Form> _forms;

        private readonly string _connectionString;
        private readonly string _databaseName;

        private readonly string _lemmasCollectionName;
        private readonly string _formsCollectionName;

        public IMongoCollection<Lemma> Lemmas => _lemmas;
        public IMongoCollection<Form> Forms => _forms;

        public MongoDbContext(IDatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
            _databaseName = string.IsNullOrEmpty(settings.DatabaseName) ? "dictionary" : settings.DatabaseName;

            _lemmasCollectionName = string.IsNullOrEmpty(settings.LemmasCollectionName) ? "lemmas" : settings.LemmasCollectionName;
            _formsCollectionName = string.IsNullOrEmpty(settings.FormsCollectionName) ? "forms" : settings.FormsCollectionName;

            _client = (string.IsNullOrEmpty(_connectionString)) ? new MongoClient() : new MongoClient(_connectionString);

            _db = _client.GetDatabase(_databaseName);

            _lemmas = _db.GetCollection<Lemma>(_lemmasCollectionName);
            _forms = _db.GetCollection<Form>(_formsCollectionName);
        }

        public Task<int> SaveChanges()
        {
            return new Task<int>(() =>
            {
                return
                _forms.Watch().ToList().Count + _lemmas.Watch().ToList().Count;
            });
        }

        public void Dispose()
        {

        }
    }
}