namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Base class for domain events.
/// </summary>
public abstract class BaseEvent
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
