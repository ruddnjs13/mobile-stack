using System;
using System.Collections.Generic;

public interface IEvent { }

public static class EventBus
{
    private static readonly Dictionary<Type, List<Delegate>> subscribers = new();

    public static void Subscribe<T>(Action<T> handler) where T : IEvent
    {
        var type = typeof(T);

        if (!subscribers.TryGetValue(type, out var handlers))
        {
            handlers = new List<Delegate>();
            subscribers[type] = handlers;
        }

        handlers.Add(handler);
    }

    public static void Unsubscribe<T>(Action<T> handler) where T : IEvent
    {
        var type = typeof(T);

        if (subscribers.TryGetValue(type, out var handlers))
        {
            handlers.Remove(handler);
        }
    }

    public static void Raise<T>(T evt) where T : IEvent
    {
        var type = typeof(T);

        if (!subscribers.TryGetValue(type, out var handlers))
            return;

        for (int i = 0; i < handlers.Count; i++)
        {
            ((Action<T>)handlers[i]).Invoke(evt);
        }
    }
}