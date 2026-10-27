public class Program
{
    public static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video v1 = new Video("Building a Telecaster Body", "Colby Guitars", 842);
        v1.AddComment(new Comment("Brayden", "That grain pattern is unreal."));
        v1.AddComment(new Comment("Emma", "What finish did you use?"));
        v1.AddComment(new Comment("Olivia", "Love watching you work."));
        videos.Add(v1);

        Video v2 = new Video("Wiring a Fuzz Pedal", "Pedal Lab", 615);
        v2.AddComment(new Comment("Sam", "Which transistors are those?"));
        v2.AddComment(new Comment("Jordan", "Great breakdown of the circuit."));
        v2.AddComment(new Comment("Riley", "Clean solder joints!"));
        v2.AddComment(new Comment("Casey", "Subscribed."));
        videos.Add(v2);

        Video v3 = new Video("Ableton Live Guitar Tracking Tips", "Studio Notes", 1230);
        v3.AddComment(new Comment("Taylor", "The gain staging tip helped a lot."));
        v3.AddComment(new Comment("Morgan", "Can you cover amp sims next?"));
        v3.AddComment(new Comment("Drew", "Super clear explanation."));
        videos.Add(v3);

        Video v4 = new Video("Routing Pickup Cavities", "Colby Guitars", 530);
        v4.AddComment(new Comment("Avery", "Template routing is the way."));
        v4.AddComment(new Comment("Quinn", "How do you avoid tear-out?"));
        v4.AddComment(new Comment("Blake", "Needed this for my build."));
        videos.Add(v4);

        foreach (Video video in videos)
        {
            Console.WriteLine(video.GetDisplayText());
            Console.WriteLine();
        }
    }
}
