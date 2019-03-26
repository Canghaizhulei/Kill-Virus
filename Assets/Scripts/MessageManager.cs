using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageManager  {

    public delegate void Act();
    public delegate void Act<T>(T t);
    public delegate void Act<T, U>(T t, U u);

    public delegate T ActRet<T>();
    public delegate T ActRet<T,U>(U u);
    public delegate T ActRet<T,U,V>(U u,V v);


    Dictionary<int, Delegate> eventTable = new Dictionary<int, Delegate>();


    private static MessageManager instance = null;
    public static MessageManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new MessageManager();
            }
            return instance;
        }
    }

    public void AddListener<T, U>(int eventType, Act<T, U> listenerBeingAdded)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType())
        {
            Debug.LogError(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
        else
        {
            eventTable[eventType] = (Act<T, U>)eventTable[eventType] + listenerBeingAdded;
        }
    }
    public void AddListener<T>(int eventType, Act<T> listenerBeingAdded)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType())
        {
            Debug.LogError(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
        else
        {
            eventTable[eventType] = (Act<T>)eventTable[eventType] + listenerBeingAdded;
        }
    }
    public void AddListener(int eventType, Act listenerBeingAdded)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType())
        {
            Debug.LogError(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
        else
        {
            eventTable[eventType] = (Act)eventTable[eventType] + listenerBeingAdded;
        }
    }
    public void AddListener<T>(int eventType, ActRet<T> listenerBeingAdded)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType())
        {
            Debug.LogError(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
        else
        {
            eventTable[eventType] = (ActRet<T>)eventTable[eventType] + listenerBeingAdded;
        }
    }
    public void AddListener<T,U>(int eventType, ActRet<T,U> listenerBeingAdded)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType())
        {
            Debug.LogError(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
        else
        {
            eventTable[eventType] = (ActRet<T,U>)eventTable[eventType] + listenerBeingAdded;
        }
    }
    public void AddListener<T,U,V>(int eventType, ActRet<T,U,V> listenerBeingAdded)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType())
        {
            Debug.LogError(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
        else
        {
            eventTable[eventType] = (ActRet<T,U,V>)eventTable[eventType] + listenerBeingAdded;
        }
    }
    public void RemoveListen(int eventType,Act listenerBeingRemoved)
    {
        if (eventTable.ContainsKey(eventType))
        {
            Delegate d = eventTable[eventType];

            if (d == null)
            {
                Debug.LogError(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                Debug.LogError(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
            else
            {
                eventTable[eventType] = (Act)eventTable[eventType] - listenerBeingRemoved;
                if (eventTable[eventType] == null)
                {
                    eventTable.Remove(eventType);
                }
            }
        }
        else
        {
            Debug.LogError(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
        }
    }
    public void RemoveListen<T>(int eventType, Act<T> listenerBeingRemoved)
    {
        if (eventTable.ContainsKey(eventType))
        {
            Delegate d = eventTable[eventType];

            if (d == null)
            {
                Debug.LogError(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                Debug.LogError(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
            else
            {
                eventTable[eventType] = (Act<T>)eventTable[eventType] - listenerBeingRemoved;
                if (eventTable[eventType] == null)
                {
                    eventTable.Remove(eventType);
                }
            }
        }
        else
        {
            Debug.LogError(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
        }
    }
    public void RemoveListen<T, U>(int eventType, Act<T, U> listenerBeingRemoved)
    {
        if (eventTable.ContainsKey(eventType))
        {
            Delegate d = eventTable[eventType];

            if (d == null)
            {
                Debug.LogError(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                Debug.LogError(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
            else
            {
                eventTable[eventType] = (Act<T, U>)eventTable[eventType] - listenerBeingRemoved;
                if (eventTable[eventType] == null)
                {
                    eventTable.Remove(eventType);
                }
            }
        }
        else
        {
            Debug.LogError(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
        }
    }
    public void RemoveListen<T>(int eventType, ActRet<T> listenerBeingRemoved)
    {
        if (eventTable.ContainsKey(eventType))
        {
            Delegate d = eventTable[eventType];

            if (d == null)
            {
                Debug.LogError(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                Debug.LogError(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
            else
            {
                // ReSharper disable once DelegateSubtraction
                eventTable[eventType] = (ActRet<T>)eventTable[eventType] - listenerBeingRemoved;
                if (eventTable[eventType] == null)
                {
                    eventTable.Remove(eventType);
                }
            }
        }
        else
        {
            Debug.LogError(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
        }
    }
    public void RemoveListen<T,U>(int eventType, ActRet<T,U> listenerBeingRemoved)
    {
        if (eventTable.ContainsKey(eventType))
        {
            Delegate d = eventTable[eventType];

            if (d == null)
            {
                Debug.LogError(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                Debug.LogError(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
            else
            {
                // ReSharper disable once DelegateSubtraction
                eventTable[eventType] = (ActRet<T,U>)eventTable[eventType] - listenerBeingRemoved;
                if (eventTable[eventType] == null)
                {
                    eventTable.Remove(eventType);
                }
            }
        }
        else
        {
            Debug.LogError(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
        }
    }
    public void RemoveListen<T,U,V>(int eventType, ActRet<T,U,V> listenerBeingRemoved)
    {
        if (eventTable.ContainsKey(eventType))
        {
            Delegate d = eventTable[eventType];

            if (d == null)
            {
                Debug.LogError(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                Debug.LogError(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
            else
            {
                // ReSharper disable once DelegateSubtraction
                eventTable[eventType] = (ActRet<T,U,V>)eventTable[eventType] - listenerBeingRemoved;
                if (eventTable[eventType] == null)
                {
                    eventTable.Remove(eventType);
                }
            }
        }
        else
        {
            Debug.LogError(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
        }
    }

    public void Dispatch(int eventType)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            ((Act)d)();
        }
    }
    public void Dispatch<T>(int eventType, T param1)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            ((Act<T>)d)(param1);
        }
    }
    public void Dispatch<T, U>(int eventType, T param1, U param2)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            ((Act<T, U>)d)(param1, param2);
        }
    }
    public T Dispatch<T>(int eventType)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            return ((ActRet<T>)d)();
        }
        return default(T);
    }
    public T Dispatch<T,U>(int eventType,U parma1)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            return ((ActRet<T,U>)d)(parma1);
        }
        return default(T);
    }
    public T Dispatch<T,U,V>(int eventType,U parma1,V parma2)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            return ((ActRet<T,U,V>)d)(parma1,parma2);
        }
        return default(T);
    }

    public void Test()
    {
        MessageManager.GetInstance.AddListener<int>(3, F);
        MessageManager.GetInstance.AddListener<int, string>(2, MyCallback);
        int a = MessageManager.GetInstance.Dispatch<int>(3);
        Debug .Log("get: " + a);
        MessageManager.GetInstance.Dispatch<int, string>(2, 55, "aaa");
    }

    int F()
    {
        Debug.Log(10);
        return 10;
    }
    private void MyCallback(int n, string s)
    {
        Debug.Log(string.Format("param1 {0}, parma2 {1}", n, s));
    }
}
