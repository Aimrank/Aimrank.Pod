namespace Aimrank.Pod.Core.Events
{
    public interface IEventsDispatcher
    {
        void Dispatch(IEvent @event);
    }
}