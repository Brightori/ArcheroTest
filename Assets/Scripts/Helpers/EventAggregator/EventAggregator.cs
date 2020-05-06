using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public delegate void EventHandler<T>(T e);

namespace GlobalEventAggregator
{
    public class EventContainer<T> : IDebugable
    {
        private event EventHandler<T> _eventKeeper;
        private readonly HashSet<OwnWeakReference<T>> _activeListenersOfThisType = new HashSet<OwnWeakReference<T>>();
        private const string Error = "null";

        public bool HasDuplicates(object listener)
        {
            return _activeListenersOfThisType.Any(k => k.holder == listener);
        }

        public void AddToEvent(object listener, EventHandler<T> action)
        {
            var newAction = new OwnWeakReference<T>(listener, action);
            _activeListenersOfThisType.Add(newAction);
            _eventKeeper += newAction.EventHandler;
        }

        public void RemoveFromEvent(object listener)
        {
            var currentEvent = _activeListenersOfThisType.FirstOrDefault(k => k.holder.Target == listener);
            if (currentEvent.holder != null)
            {
                _eventKeeper -= currentEvent.EventHandler;
                _activeListenersOfThisType.Remove(currentEvent);
            }
        }

        public EventContainer(object listener, EventHandler<T> action)
        {
            _eventKeeper += action;
            _activeListenersOfThisType.Add(new OwnWeakReference<T>(listener, action));
        }

        public void Invoke(T t)
        {
            if (_activeListenersOfThisType.Any(k=> k.IsDead()))
            {
                var failObjList = _activeListenersOfThisType.Where(k => k.IsDead());
                foreach (var fail in failObjList.ToArray())
                {
                    _eventKeeper -= fail.EventHandler;
                    _activeListenersOfThisType.Remove(fail);
                }
            }

            if (_eventKeeper != null)
                _eventKeeper(t);
            return;
        }

        public string DebugInfo()
        {
            string info = string.Empty;
            info += _activeListenersOfThisType.Count.ToString() + "Count \n";
            foreach (var c in _activeListenersOfThisType)
            {
                if (c.holder.Target != null)
                    info += c.holder.Target.ToString() + "\n";
            }
            return info;
        }
    }

    public static class EventAggregator
    {
        private static Dictionary<Type, object> GlobalListeners = new Dictionary<Type, object>();
        private static Dictionary<Type, List<ActionContainer>> DelayedInjection = new Dictionary<Type, List<ActionContainer>>();
        private static Dictionary<Type, object> SingleInjections = new Dictionary<Type, dynamic>();

        static EventAggregator()
        {
            SceneManager.sceneUnloaded += ClearGlobalListeners;
        }

        private static void ClearGlobalListeners(Scene scene)
        {
            ForceClean();
        }

        public static void ForceClean()
        {
            GlobalListeners = new Dictionary<Type, object>();
            DelayedInjection = new Dictionary<Type, List<ActionContainer>>();
            SingleInjections = new Dictionary<Type, dynamic>();
        }

        public static void AddListener<T>(object listener, Action<T> action)
        {
            var key = typeof(T);
            EventHandler<T> handler = new EventHandler<T>(action);

            if (GlobalListeners.ContainsKey(key))
            {
                var lr = (EventContainer<T>)GlobalListeners[key];
                if (lr.HasDuplicates(listener))
                    return;
                lr.AddToEvent(listener, handler);
                return;
            }
            GlobalListeners.Add(key, new EventContainer<T>(listener, handler));
        }

        public static void Invoke<T>(T data)
        {
            var key = typeof(T);
            if (!GlobalListeners.ContainsKey(key))
                return;
            var eventContainer = (EventContainer<T>)GlobalListeners[key];
            eventContainer.Invoke(data);
        }

        public static void RegisterInject<T>(T obj)
        {
            var key = typeof(T);
            if (DelayedInjection.ContainsKey(key)) 
            {
                foreach (var delayed in DelayedInjection[key])
                    delayed.InjectFunc(obj);

                DelayedInjection[key].Clear();
                DelayedInjection.Remove(key);
            }

            SingleInjections.Add(key, obj);
        }
        
        public static void Inject<T>(Action<T> obj)
        {
            var key = typeof(T);

            if (SingleInjections.ContainsKey(key))
            {
                if (SingleInjections[key] == null)
                {
                    SingleInjections.Remove(key);
                    Inject(obj);
                    return;
                }
                    
                obj.Invoke((T)SingleInjections[key]);
            }
            else
            {
                if (DelayedInjection.ContainsKey(key))
                     DelayedInjection[key].Add(new InjectAction<T>(obj));
                else
                    DelayedInjection.Add(key, new List<ActionContainer> { new InjectAction<T>(obj)});
            }
        }

        public static void RemoveListener<T>(object listener)
        {
            var key = typeof(T);
            if (GlobalListeners.ContainsKey(key))
            {
                var eventContainer = (EventContainer<T>)GlobalListeners[key];
                eventContainer.RemoveFromEvent(listener);
            }
        }

        public static void RegisterObjectByEvent<T>(T registerObject, bool stateType)
        {
            Invoke(new Register<T> { RegisterObject = registerObject, Add = stateType });
        }

        public static void RecieveRegisterObject<T>(object owner, List<T> collection)
        {
            AddListener(owner, (Register<T> obb) => collection.RegisterEventObjectAtList(obb));
        }

        public static void RecieveRegisterObject<T>(object owner, HashSet<T> collection)
        {
            AddListener(owner, (Register<T> obb) => collection.RegisterEventObjectAtList(obb));
        }

        public static void AddOrRemoveElement<T>(this List<T> list, T element, bool add)
        {
            if (add)
            {
                if (list.Contains(element))
                    return;
                list.Add(element);
            }
            else
                if (list.Contains(element))
                list.Remove(element);
        }

        public static void AddOrRemoveElement<T>(this HashSet<T> list, T element, bool add)
        {
            if (add)
            {
                if (list.Contains(element))
                    return;
                list.Add(element);
            }
            else
                if (list.Contains(element))
                list.Remove(element);
        }

        public static void RegisterEventObjectAtList<T>(this List<T> registrationList, Register<T> registerEvent)
        {
            if (registerEvent.Add)
            {
                if (registrationList.Contains(registerEvent.RegisterObject))
                    return;
                registrationList.Add(registerEvent.RegisterObject);
            }
            else
                if (registrationList.Contains(registerEvent.RegisterObject))
                    registrationList.Remove(registerEvent.RegisterObject);
        }
        public static void RegisterEventObjectAtList<T>(this HashSet<T> registrationList, Register<T> registerEvent)
        {
            if (registerEvent.Add)
            {
                if (registrationList.Contains(registerEvent.RegisterObject))
                    return;
                registrationList.Add(registerEvent.RegisterObject);
            }
            else
                if (registrationList.Contains(registerEvent.RegisterObject))
                    registrationList.Remove(registerEvent.RegisterObject);
        }

        public static string DebugInfo()
        {
            string info = string.Empty;

            foreach (var listener in GlobalListeners)
            {
                info += "тип на который подписаны объекты " +  listener.Key.ToString() + "\n";
                var t = (IDebugable)listener.Value;
                info += t.DebugInfo() + "\n";
            }

            return info;
        }
    }

    public struct OwnWeakReference<T>
    {
        public WeakReference holder;
        public EventHandler<T> EventHandler;

        public OwnWeakReference(object obj, EventHandler<T> eventHandler)
        {
            holder = new WeakReference(obj);
            EventHandler = eventHandler;
        }

        public bool IsDead()
        {
            return holder.Target == null || holder.Target.ToString() == "null" || !holder.IsAlive;
        }
    }

    public struct Register<T>
    {
        public T RegisterObject;
        public bool Add;

        public Register(T registerObject, bool add)
        {
            RegisterObject = registerObject;
            Add = add;
        }
    }

    public interface IDebugable
    {
        string DebugInfo();
    }

    public abstract class ActionContainer
    {
        public abstract void InjectFunc(object call);
    }

    public class InjectAction<T> : ActionContainer
    {
        public InjectAction(Action<T> inject)
        {
            Inject = inject;
        }

        public Action<T> Inject { get; private set; }
        public override void InjectFunc(object call)
        {
            Inject?.Invoke((T)call);
        }
    }

}

