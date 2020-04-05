using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dictionary.Core
{
    public abstract class Base
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
