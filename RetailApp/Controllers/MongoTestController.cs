using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;

[ApiController]
[Route("api/[controller]")]
public class MongoTestController : ControllerBase
{
    [HttpGet("ping")]
    public async Task<IActionResult> PingMongo()
    {
        var host = "cluster0-shard-00-00.r7aq8ix.mongodb.net";
        var port = 27017;
        var results = new List<string>();

        try
        {
            var addresses = await Dns.GetHostAddressesAsync(host);
            foreach (var addr in addresses)
            {
                results.Add($"Resolved IP: {addr}");
            }

            using (var tcpClient = new TcpClient())
            {
                await tcpClient.ConnectAsync(host, port);
                results.Add("Connection to MongoDB succeeded.");
            }

            return Ok(results);
        }
        catch (Exception ex)
        {
            results.Add($"Error: {ex.Message}");
            return StatusCode(500, results);
        }
    }
}
