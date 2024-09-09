
namespace Events
{
    public record ThermostatTempChangeEvent
    {
        public Guid CorrelationId { get; init; }
        public decimal Temperature { get; init; }
    }
}
