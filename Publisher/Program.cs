
using Events;
using MassTransit;


// Central application just for act as a event trigger

var busControl = Bus.Factory.CreateUsingRabbitMq();

try
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Choose an action:");
        Console.WriteLine("1. Turn lights On");
        Console.WriteLine("2. Turn lights Off");
        Console.WriteLine("3. Adjust Thermostat");
        Console.WriteLine("4. Exit");

        var choise = Console.ReadLine();
        switch(choise)
        {
            case "1":
                await ControlLights(busControl, LightState.On);
                break;
            case "2":
                await ControlLights(busControl, LightState.Off);
                break;
            case "3":
                Console.WriteLine("Enter new thermostat temparature: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newTemperature))
                {
                    await ControlThermostat(busControl, newTemperature);
                }
                else
                {
                    Console.WriteLine("Please enter a valid temperature value.");
                }                
                break;
            case "4":
                return;
            default:
                Console.WriteLine("Please select a valid option.");
                break;
        }
    }
}
finally
{
    await busControl.StopAsync();
}


async Task ControlLights(IBusControl busControl, LightState state)
{
    var endpoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/lights"));
    await endpoint.Send<LightSwitchEvent>(new {state = state});
    Console.WriteLine($"Sent command to turn the lights {state}");
}

async Task ControlThermostat(IBusControl busControl, decimal newTemperature)
{
    var endpoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/thermostat"));
    await endpoint.Send<ThermostatTempChangeEvent>(new { temperature = newTemperature });
    Console.WriteLine($"Sent command to adjust temperature to {newTemperature} C");
}