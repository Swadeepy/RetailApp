using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RetailApp.Data;
using RetailApp.DTO;
using RetailApp.Models.Mongo;
using RetailApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace RetailApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly MongoDbContext _mongo;
        private readonly ProductService _productService;
        private readonly IWebHostEnvironment _env;

        public ProductsController(MongoDbContext mongo, ProductService productService, IWebHostEnvironment env)
        {
            _mongo = mongo;
            _productService = productService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var products = await _mongo.Products.Find(_ => true).ToListAsync();

            foreach (var product in products)
            {
                product.ImageUrls = product.ImageUrls
                    .Select(url => $"{baseUrl}{url}")
                    .ToList();
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _mongo.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null) return NotFound();

            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            product.ImageUrls = product.ImageUrls.Select(url => $"{baseUrl}{url}").ToList();

            return Ok(product);
        }

      
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductDto dto)
        {
            await _productService.SaveProductAsync(dto);
            return Ok("Product saved");
        }
    }

}
