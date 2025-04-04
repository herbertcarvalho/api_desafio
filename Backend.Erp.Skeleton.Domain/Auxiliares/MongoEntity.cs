using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Backend.Erp.Skeleton.Domain.Auxiliares
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }

    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }

    public class Logs : Document
    {
        public bool Success { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Controller { get; set; }
        public string QueryString { get; set; }
        public object RequestBody { get; set; }
        public object ResponseBody { get; set; }
        public object RequestHeaders { get; set; }
        public string CreatedAt { get; set; } = string.Format("{0:s}", DateTime.UtcNow);
        public string Error { get; set; }
    }
}
