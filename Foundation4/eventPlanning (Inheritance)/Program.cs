public class Program
{
    public static void Main(string[] args)
    {
        Address a1 = new Address("525 S Center St", "Rexburg", "ID", "USA");
        Address a2 = new Address("200 E South Temple", "Salt Lake City", "UT", "USA");
        Address a3 = new Address("Porter Park, 400 W 7th S", "Rexburg", "ID", "USA");

        Lecture lecture = new Lecture("Intro to Embedded Systems", "A talk on building ESP32 projects.", "Nov 12, 2026", "6:00 PM", a1, "Dr. Rivera", 120);
        Reception reception = new Reception("Engineering Mixer", "Meet faculty and alumni over refreshments.", "Nov 20, 2026", "7:30 PM", a2, "rsvp@engineeringmixer.example");
        OutdoorGathering gathering = new OutdoorGathering("Fall Picnic", "Food, music, and lawn games.", "Oct 31, 2026", "12:00 PM", a3, "Sunny, 62 F");

        List<Event> events = new List<Event> { lecture, reception, gathering };

        foreach (Event e in events)
        {
            Console.WriteLine("=== Standard Details ===");
            Console.WriteLine(e.GetStandardDetails());
            Console.WriteLine();
            Console.WriteLine("=== Full Details ===");
            Console.WriteLine(e.GetFullDetails());
            Console.WriteLine();
            Console.WriteLine("=== Short Description ===");
            Console.WriteLine(e.GetShortDescription());
            Console.WriteLine("----------------------------------");
        }
    }
}
