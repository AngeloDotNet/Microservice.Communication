using MassTransit;
using Microservice.Consumer.Consumer;
using Microservice.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(ShoppingCartConsumer).Assembly);
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(RabbitConfiguration.HostName), h =>
        {
            h.Username(RabbitConfiguration.Username);
            h.Password(RabbitConfiguration.Password);
        });

        config.ReceiveEndpoint(RabbitConfiguration.QueueName, ep =>
        {
            ep.PrefetchCount = RabbitConfiguration.PrefetchCount;
            ep.UseMessageRetry(r => r.Interval(RabbitConfiguration.RetryCount, RabbitConfiguration.RetryInterval));
            ep.ConfigureConsumers(context);
        });
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();