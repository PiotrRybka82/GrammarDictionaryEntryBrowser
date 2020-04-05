using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dictionary.Core
{
    public class Lemma : Base
    {
        [BsonElement("lemma")]
        public string Form { get; set; }
        [BsonElement("tag")]
        public string Tag { get; set; }

    }
}