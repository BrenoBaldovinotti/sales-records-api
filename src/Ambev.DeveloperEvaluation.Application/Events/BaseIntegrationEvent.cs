namespace Ambev.DeveloperEvaluation.Application.Events
{

    /// <summary>
    /// Base class for integration events.
    /// </summary>
    public abstract class BaseIntegrationEvent
    {
        /// <summary>
        /// The unique event ID.
        /// </summary>
        public Guid EventId { get; } = Guid.NewGuid();

        /// <summary>
        /// The time when the event occurred.
        /// </summary>
        public DateTime OccurredAt { get; } = DateTime.UtcNow;
    }
}