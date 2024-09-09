using Events;
using MassTransit;

namespace LightControlService
{
    public class LightSwitchEventSubscriber : IConsumer<LightSwitchEvent>
    {
        public async Task Consume(ConsumeContext<LightSwitchEvent> context)
        {
            var lightEvent = context.Message;

            var sucessful = await ControlLightAsync(lightEvent);
            if (sucessful)
            {
                Console.WriteLine($"Light switched {lightEvent.State} successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to control lights {lightEvent.State}.");
            }
        }

        private async Task<bool> ControlLightAsync(LightSwitchEvent lightEvent)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                if (lightEvent.State == LightState.On)
                {
                    Console.WriteLine("Turning lights ON...");
                }
                else
                {
                    Console.WriteLine("Turning lights OFF...");
                }
                return true;
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error controlling lights: {ex.Message}");
                return false;
            }
        }
    }
}
