using System;

namespace Aimrank.Pod.Core.Events
{
    public interface IEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
    }
}