namespace RetailApp.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Category { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }

        // For image upload via form
        public List<IFormFile> Files { get; set; }
        public string? BaseUrl { get; set; }
    }
}
