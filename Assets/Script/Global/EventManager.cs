using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Dictionary<EventID, List<EventObserver>> ObserverMap=new Dictionary<EventID, List<EventObserver>>();
    Queue eventQueue = new Queue(); //��Ϣ����
    private static EventManager _instance = null;
    public static EventManager instance()
    {
        return _instance;
    }
    void Awake()
    {
        Debug.Log("===========������Ϣϵͳ===========");
        _instance = this;
    }
    void Start()
    {
       
    }

    void Update()
    {
        while (eventQueue.Count > 0)
        {
            EventData eve = (EventData)eventQueue.Dequeue();
            if (!ObserverMap.ContainsKey(eve.eid))
            {
                continue;
            }
            List<EventObserver> observers = ObserverMap[eve.eid];
            for (int i = 0; i < observers.Count; i++)
            {
                if (observers[i] == null) continue;
                observers[i].HandleEvent(eve);
            }
        }
    }
    public void SendEvent(EventData eve)
    {
        eventQueue.Enqueue(eve);
    }

    //��Ӽ�����
    void RegisterObj(EventObserver newobj, EventID eid)
    {
        if (!ObserverMap.ContainsKey(eid))
        {
            List<EventObserver> list = new List<EventObserver>
            {
                newobj
            };
            ObserverMap.Add(eid, list);
        }
        else
        {
            List<EventObserver> list = ObserverMap[eid];
            foreach (EventObserver obj in list)
            {
                if (obj == newobj)
                {
                    return;
                }
            }
            list.Add(newobj);
        }
    }
    //�Ƴ�������
    void RemoveObj(EventObserver removeobj)
    {
        foreach (KeyValuePair<EventID, List<EventObserver>> kv in ObserverMap)
        {
            List<EventObserver> list = kv.Value;
            foreach (EventObserver obj in list)
            {
                if (obj == removeobj)
                {
                    list.Remove(obj);
                    break;
                }
            }
        }
    }
    /// <summary>
    /// �Ƴ�һ��������
    /// </summary>
    /// <returns>The remove.</returns>
    /// <param name="removeobj">Removeobj.</param>
	public static void Remove(EventObserver removeobj)
    {
        if (EventManager.instance() == null) return;
        EventManager.instance().RemoveObj(removeobj);
    }
    /// <summary>
    /// ������������ע��
    /// </summary>
    /// <returns>The register.</returns>
    /// <param name="newobj">Newobj.</param>
    /// <param name="eids">��Ҫ�������¼��б�.</param>
    public static void Register(EventObserver newobj, params EventID[] eids)
    {
        if (EventManager.instance() == null) return;
        foreach (EventID eid in eids)
        {
            EventManager.instance().RegisterObj(newobj, eid);
        }
    }
}
