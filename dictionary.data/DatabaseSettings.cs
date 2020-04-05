using Dictionary.Core;

namespace Dictionary.Data
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string FormsCollectionName { get; set; }
        public string LemmasCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}