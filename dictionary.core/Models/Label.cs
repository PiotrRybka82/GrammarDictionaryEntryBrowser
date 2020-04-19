using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dictionary.Core.Models
{
    public class Label : Base
    {
        [BsonElement("key")]
        public string Key { get; set; }

        [BsonElement("valueAbbr")]
        public string ValueAbbr { get; set; }

        [BsonElement("valueFull")]
        public string ValueFull { get; set; }

        [BsonElement("categoryName")]
        public string CategoryName { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}