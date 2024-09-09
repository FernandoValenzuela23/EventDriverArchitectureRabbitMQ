using Events;
using MassTransit;

namespace ThermostatControlService
{
    public class ThermostatEventSubscriber : IConsumer<ThermostatTempChangeEvent>
    {
        public async Task Consume(ConsumeContext<ThermostatTempChangeEvent> context)
        {
            var thermostatEvent = context.Message;

            var sucessful = await AdjustThermostatAsync(thermostatEvent);
            if (sucessful)
            {
                Console.WriteLine($"Temperature changed to {thermostatEvent.Temperature} C successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to adjust temperature {thermostatEvent.Temperature}.");
            }
        }

        private async Task<bool> AdjustThermostatAsync(ThermostatTempChangeEvent thermostatEvent)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                Console.WriteLine($"Adjusting thermostat to {thermostatEvent.Temperature} C...");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adjusting thermostat: {ex.Message}");
                return false;
            }
        }
    }
}
