using MongoDB.Bson.Serialization.Attributes;

namespace Mimamori.Domains.Entities;

public class BaseAttributes
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}