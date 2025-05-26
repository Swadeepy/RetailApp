using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RetailApp.Models.Mongo
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> ImageUrls { get; set; } = new();

        public DateTime CreatedDate { get; set; }
    }
}
