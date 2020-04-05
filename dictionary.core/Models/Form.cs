using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dictionary.Core.Models
{
    public class Form : Base
    {
        [BsonElement("form")]
        public string Word { get; set; }
        [BsonElement("lemma")]
        public Lemma Lemma { get; set; }
        [BsonElement("categories")]
        public IEnumerable<string> Categories { get; set; }
        [BsonElement("meanings")]
        public IEnumerable<string> Meanings { get; set; }
        [BsonElement("labels")]
        public IEnumerable<string> Labels { get; set; }
    }
}