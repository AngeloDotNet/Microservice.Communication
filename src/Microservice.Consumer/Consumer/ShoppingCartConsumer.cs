using MassTransit;
using Microservice.Shared;

namespace Microservice.Consumer.Consumer;

public class ShoppingCartConsumer : IConsumer<Product>
{
    public Task Consume(ConsumeContext<Product> context)
    {
        var data = context.Message;

        return Task.CompletedTask;
    }
}