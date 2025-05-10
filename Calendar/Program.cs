
using System;
using System.Globalization;
using Events;

class Program
{
    static void Main(string[] args)
    {
        List<Event> events = new List<Event>();
        Console.WriteLine("\nWelcome to the Calendar App!");
        while (true)
        {
            Console.WriteLine("\nList of options: \n1. EVENT\n2. LIST\n3. STATS\n4. END");
            Console.Write("\nOption: ");
            string[] parts = Console.ReadLine().Trim().Split(';');
            if (parts[0] == "EVENT" && parts.Length < 3)
            {
                Console.WriteLine("Invalid input. Please provide an event with correct number of parameters.");
                continue;
            }
            switch (parts[0])
            {
                case "EVENT":
                    string eventName = string.Empty;
                    DateTime eventDate = DateTime.MinValue;

                    if (!string.IsNullOrWhiteSpace(parts[1])) eventName = parts[1];
                    else
                    {
                        Console.WriteLine("Event name cannot be empty.");
                        break;
                    }
                    if (!DateTime.TryParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out eventDate))
                    {
                        Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                        break;
                    }

                    Events.Event newEvent = new Events.Event(eventName, eventDate);
                    events.Add(newEvent);
                    break;

                case "LIST":
                    if (events.Count == 0)
                    {
                        Console.WriteLine("No events to display");
                    }
                    else
                    {
                        Console.WriteLine("Events:");
                        events.Sort((x, y) => x.Date.CompareTo(y.Date));
                        foreach (var ev in events)
                        {
                            TimeSpan timeSpan = ev.Date.Subtract(DateTime.Today);
                            if (timeSpan.Days > 0)
                            {
                                Console.WriteLine($"Event {ev.EventName} with date {ev.Date.ToString("yyyy-MM-dd")} will happen in {timeSpan.Days} days.");
                            }
                            else if (timeSpan.Days < 0)
                            {
                                Console.WriteLine($"Event {ev.EventName} with date {ev.Date.ToString("yyyy-MM-dd")} happened {timeSpan.Days * (-1)} ago.");
                            }
                            else
                            {
                                Console.WriteLine($"Event {ev.EventName} with date {ev.Date.ToString("yyyy-MM-dd")} is today.");
                            }
                        }
                    }
                    break;

                case "STATS":
                    Dictionary<DateTime, int> eventCount = new Dictionary<DateTime, int>();
                    foreach (var ev in events)
                    {
                        if (eventCount.ContainsKey(ev.Date))
                        {
                            eventCount[ev.Date]++;
                        }
                        else
                        {
                            eventCount[ev.Date] = 1;
                        }
                    }
                    var orderedDict = eventCount.OrderBy(e => e.Key);
                    foreach (var ev in orderedDict)
                    {
                        Console.WriteLine($"Date: {ev.Key.ToString("yyyy-MM-dd")}: {ev.Value} events.");
                    }
                    break;

                case "END":
                    Console.WriteLine("Ending the application.");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}