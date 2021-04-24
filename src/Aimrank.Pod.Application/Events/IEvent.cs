using System;

namespace Aimrank.Pod.Application.Events
{
    public interface IEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
    }
}