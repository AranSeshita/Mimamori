using MongoDB.Bson.Serialization.Attributes;

namespace Mimamori.Domains.Entities;

public class BaseAttributes
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime? UpdatedOn { get; set; }
}