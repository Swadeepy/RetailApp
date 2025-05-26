using RetailApp.Data;
using RetailApp.Data.Repositories;
using RetailApp.DTO;
using RetailApp.Models.Mongo;

namespace RetailApp.Services
{
    public class ProductService
    {
        private readonly MongoDbContext _mongo;
        private readonly IWebHostEnvironment _env;

        public ProductService(MongoDbContext mongo, IWebHostEnvironment env)
        {
            _mongo = mongo;
            _env = env;
        }

        public async Task SaveProductAsync(ProductDto dto)
        {
            var imageUrls = new List<string>();

            if (dto.Files != null && dto.Files.Count > 0)
            {
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsPath);

                foreach (var image in dto.Files)
                {
                    var uniqueName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(uploadsPath, uniqueName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);

                    imageUrls.Add($"/uploads/{uniqueName}");
                }
            }

            var product = new Product
            {
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrls = imageUrls,
                CreatedDate = DateTime.UtcNow
            };

            await _mongo.Products.InsertOneAsync(product);
        }
    }
}
