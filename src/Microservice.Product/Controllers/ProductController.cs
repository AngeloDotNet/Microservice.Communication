using MassTransit;
using Microservice.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Product.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IBus bus) : ControllerBase
{
    private readonly IBus bus = bus;

    [HttpPost]
    public async Task<IActionResult> CreateCartAsync(Shared.Product product)
    {
        if (product != null)
        {
            var uri = new Uri(RabbitConfiguration.EndpointCart);
            var endPoint = await bus.GetSendEndpoint(uri);
            await endPoint.Send(product);
            return Ok();
        }

        return BadRequest();
    }
}