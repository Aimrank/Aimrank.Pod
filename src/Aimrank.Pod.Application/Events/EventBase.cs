using System;

namespace Aimrank.Pod.Application.Events
{
    public abstract class EventBase : IEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OccurredAt { get; } = DateTime.UtcNow;
    }
}