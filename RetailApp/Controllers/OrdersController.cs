using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailApp.Data;
using RetailApp.Models.SQL;

namespace RetailApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public OrdersController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return Ok(order);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var orders = await _db.Orders
                                  .Include(o => o.Items)
                                  .Where(o => o.UserId == userId)
                                  .ToListAsync();
            return Ok(orders);
        }
    }

}
