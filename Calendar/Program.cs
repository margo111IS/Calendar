
using System;
using Events;

//1. accept multiple events at once?
//2. to prompt user after error?
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
            //string[] parts = new string[] { "EVENT", "Birthday Party", "2023-10-" };
            switch (parts[0])
            {
                case "EVENT":
                    Console.WriteLine("You selected EVENT.meow");
                    string eventName = string.Empty;
                    DateTime eventDate = DateTime.MinValue;
                    int[] date = null;//parts[2].Split('-').Select(int.Parse).ToArray();
                    try
                    {
                        date = Array.ConvertAll(parts[2].Split('-'), int.Parse);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                        break;
                    }
                    if (!string.IsNullOrEmpty(parts[1])) eventName = parts[1];
                    else
                    {
                        Console.WriteLine("Event name cannot be empty.");
                        break;
                    }
                    foreach (var d in parts[2].Split('-'))
                    {
                        if (!int.TryParse(d, out int result) || result < 0)
                        {
                            Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                            break;
                        }
                    }
                    if (date.Length == 3 && date[0] > 0 && date[1] > 0 && date[2] > 0 && date[1] <= 12 && date[2] <= 31)
                    {
                        eventDate = new DateTime(date[0], date[1], date[2]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                        break;
                    }
                    Events.Event newEvent = new Events.Event(eventName, eventDate);
                    events.Add(newEvent);
                    break;
                case "LIST":
                    Console.WriteLine("You selected LIST.");
                    if (events.Count == 0)
                    {
                        Console.WriteLine("No events to display");
                    }
                    else
                    {
                        Console.WriteLine("Events:");
                        foreach (var ev in events)
                        {
                            TimeSpan timeSpan = ev.Date.Subtract(DateTime.Now);
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
                    Console.WriteLine("You selected STATS.");
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