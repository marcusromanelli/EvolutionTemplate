using System;
using Zenject;

public delegate void OnEventTrigger(EventType type, params object[] param);


public interface IEventHandler : IInitializable, ITickable, IDisposable {
    event OnEventTrigger _onEventTrigger;

    event OnEventTrigger onEventTrigger;

    void TriggerEvent(EventType type, params object[] param);
}
