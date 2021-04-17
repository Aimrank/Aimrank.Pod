namespace Aimrank.Pod.Application.Events
{
    public interface IEventDispatcher
    {
        void Dispatch(IEvent @event);
    }
}