
using MassTransit;
using ThermostatControlService;

// Creating Thermostat service endpoint (thermostat)
var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost", 5672, "/", h =>
    {
        h.Username("guest");
        h.Password("guest");
    });

    cfg.ReceiveEndpoint("thermostat", e =>
    {
        e.Consumer<ThermostatEventSubscriber>();
    });
});

await busControl.StartAsync();

Console.WriteLine("Thermostat control service is running. Pres any key to exit.");
Console.ReadLine();

await busControl.StopAsync();