namespace Ambev.DeveloperEvaluation.Application.Events;

/// <summary>
/// Interface for publishing events.
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// Publishes a domain or integration event.
    /// </summary>
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class;
}
