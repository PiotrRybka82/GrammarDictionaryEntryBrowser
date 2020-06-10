using System;

namespace Dictionary.Core
{
    public interface IDatabaseSettings
    {
        string FormsCollectionName { get; set; }
        string LemmasCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}