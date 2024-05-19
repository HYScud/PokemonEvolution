public class EventData
{
    public EventID eid;
    public EventData(EventID eid)
    {
        this.eid = eid;
    }

    public void Send()
    {
        if (EventManager.instance() != null) EventManager.instance().SendEvent(this);
    }
    public static EventData CreateEvent(EventID eventid)
    {
        EventData data = new EventData(eventid);
        return data;
    }
}