using System.Collections.Generic;
using BaseFramework;
using UnityEngine.Events;

namespace BaseFramework
{
    public interface IEventInfo
    {
    
    }
    /// <summary>
    /// 需要传递参数的事件
    /// </summary>
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }
    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }
}

public class EventManager : SingletonAutoMono<EventManager>
{
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();
    /// <summary>
    /// 添加包含参数的事件监听   
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">包含的函数</param>
    /// <typeparam name="T">参数类型</typeparam>
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.TryGetValue(name, out IEventInfo value))
        {
            (value as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }
    /// <summary>
    /// 添加事件监听，无参数
    /// </summary>
    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.TryGetValue(name, out IEventInfo value))
        {
            (value as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }
    /// <summary>
    /// 移除事件监听，有参
    /// </summary>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.TryGetValue(name, out IEventInfo value))
        {
            (value as EventInfo<T>).actions -= action;
        }
    }
    /// <summary>
    /// 移除事件监听，无参
    /// </summary>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.TryGetValue(name, out IEventInfo value))
        {
            (value as EventInfo).actions -= action;
        }
    }
    /// <summary>
    /// 事件触发函数，有参
    /// </summary>
    public void EventTrigger<T>(string name, T info)
    {
        if (eventDic.TryGetValue(name, out IEventInfo value))
        {
            EventInfo<T> eventInfo = value as EventInfo<T>;
            if (eventInfo!= null && eventInfo.actions != null)
            {
                eventInfo.actions.Invoke(info);
            }
        }
    }
    /// <summary>
    /// 事件触发函数，无参
    /// </summary>
    public void EventTrigger(string name)
    {
        if (eventDic.TryGetValue(name, out IEventInfo value))
        {
            EventInfo eventInfo = value as EventInfo;
            if (eventInfo != null && eventInfo.actions != null)
            {
                eventInfo.actions.Invoke();
            }
        }
    }
    /// <summary>
    /// 清空事件中心，一般在切换场景时调用
    /// </summary>
    public void ClearEvent()
    {
        eventDic.Clear();
    }

}
