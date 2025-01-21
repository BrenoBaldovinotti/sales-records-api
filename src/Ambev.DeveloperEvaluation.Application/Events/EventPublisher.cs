using Ambev.DeveloperEvaluation.Application.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Infrastructure.Events;

/// <summary>
/// Mock implementation for publishing events.
/// </summary>
public class EventPublisher : IEventPublisher
{
    private readonly ILogger<EventPublisher> _logger;

    public EventPublisher(ILogger<EventPublisher> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Publishes the event by logging it.
    /// </summary>
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class
    {
        _logger.LogInformation("Published event: {EventType} - Data: {@Event}", typeof(TEvent).Name, @event);
        return Task.CompletedTask;
    }
}
