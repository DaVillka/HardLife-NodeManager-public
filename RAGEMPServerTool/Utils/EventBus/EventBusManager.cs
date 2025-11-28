using System;
using System.Collections.Generic;
using System.Linq;

public interface IGlobalSubscriber { }
internal class SubscribersList<TSubscriber> where TSubscriber : class
{
    private bool m_NeedsCleanUp = false;

    public bool Executing;

    public readonly List<TSubscriber> List = new List<TSubscriber>();

    public void Add(TSubscriber subscriber)
    {
        List.Add(subscriber);
    }

    public void Remove(TSubscriber subscriber)
    {
        if (Executing)
        {
            var i = List.IndexOf(subscriber);
            if (i >= 0)
            {
                m_NeedsCleanUp = true;
                List[i] = null;
            }
        }
        else
        {
            List.Remove(subscriber);
        }
    }

    public void Cleanup()
    {
        if (!m_NeedsCleanUp)
        {
            return;
        }

        List.RemoveAll(s => s == null);
        m_NeedsCleanUp = false;
    }
}
internal static class EventBusHelper
{
    private static readonly Dictionary<Type, List<Type>> s_CashedSubscriberTypes = new Dictionary<Type, List<Type>>();

    public static List<Type> GetSubscriberTypes(IGlobalSubscriber globalSubscriber)
    {
        Type type = globalSubscriber.GetType();
        if (s_CashedSubscriberTypes.ContainsKey(type))
            return s_CashedSubscriberTypes[type];

        List<Type> subscriberTypes = type
                .GetInterfaces()
                .Where(t => t.GetInterfaces()
                    .Contains(typeof(IGlobalSubscriber)))
                .ToList();

        s_CashedSubscriberTypes[type] = subscriberTypes;
        return subscriberTypes;
    }
}
internal static class EventBusManager
{
    private static readonly Dictionary<Type, SubscribersList<IGlobalSubscriber>> s_Subscribers = new Dictionary<Type, SubscribersList<IGlobalSubscriber>>();

    public static void Subscribe(IGlobalSubscriber subscriber)
    {
        List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
        foreach (Type t in subscriberTypes)
        {
            if (!s_Subscribers.ContainsKey(t))
                s_Subscribers[t] = new SubscribersList<IGlobalSubscriber>();
            s_Subscribers[t].Add(subscriber);
        }
    }

    public static void Unsubscribe(IGlobalSubscriber subscriber)
    {
        List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
        foreach (Type t in subscriberTypes)
        {
            if (s_Subscribers.ContainsKey(t))
                s_Subscribers[t].Remove(subscriber);
        }
    }

    public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, IGlobalSubscriber
    {
        SubscribersList<IGlobalSubscriber> subscribers = s_Subscribers.ContainsKey(typeof(TSubscriber)) ? s_Subscribers[typeof(TSubscriber)] : null;
        if (subscribers == null) return;
        subscribers.Executing = true;
        foreach (IGlobalSubscriber subscriber in subscribers.List)
        {
            try { action.Invoke(subscriber as TSubscriber); }
            catch (Exception e) { Console.WriteLine(e.ToString());/*Debug.LogError(e);*/}
        }
        subscribers.Executing = false;
        subscribers.Cleanup();
    }
}

