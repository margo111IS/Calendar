using System;
using System.Collections.Generic;

namespace Events
{
    public class Event
    {
        public string EventName;
        public DateTime Date;
        public Event(string eventName, DateTime date)
        {
            EventName = eventName;
            Date = date;
        }
    }
}
