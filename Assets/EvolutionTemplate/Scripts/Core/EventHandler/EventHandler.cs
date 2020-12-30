using System;
using UnityEngine;
using Zenject;

/// <summary>
/// The EventHandler is a simple way to achieve communication between UI, Data Structures to the game itself without using fixed links between classes.
/// </summary>
public class EventHandler : IEventHandler {
    public event OnEventTrigger _onEventTrigger;

    public event OnEventTrigger onEventTrigger
    {
        add
        {
            _onEventTrigger += value;
        }

        remove
        {
            _onEventTrigger -= value;
        }
    }

    public void Dispose()
    {
    }

    public void Initialize()
    {
    }

    public void Tick()
    {
    }


    public void TriggerEvent(EventType type, params object[] param)
    {
        Debug.Log("Triggered " + type.ToString());
        _onEventTrigger(type, param);
    }
}
