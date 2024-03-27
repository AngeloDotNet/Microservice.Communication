namespace Microservice.Shared;
public static class RabbitConfiguration
{
    public static string HostName => "rabbitmq://localhost";
    public static string Username => "guest";
    public static string Password => "guest";
    public static string QueueName => "cart";
    public static int PrefetchCount => 16;
    public static int RetryCount => 3;
    public static int RetryInterval => 100;
    public static string EndpointCart => string.Concat(HostName, "/cart");
}
