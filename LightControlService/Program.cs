
using LightControlService;
using MassTransit;

// Creating Light service endpoint (ligths)
var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{    
    cfg.Host("localhost", 5672, "/", h =>
    {
        h.Username("guest");
        h.Password("guest");
    });

    
    cfg.ReceiveEndpoint("lights", e =>
    {
        e.Consumer<LightSwitchEventSubscriber>();
    });
});

await busControl.StartAsync();

Console.WriteLine("Light control service is running. Pres any key to exit.");
Console.ReadLine();

await busControl.StopAsync();