namespace Events
{
    public record LightSwitchEvent
    {
        public Guid CorrelatinId { get; init; }
        public LightState State { get; init; }

    }
}
