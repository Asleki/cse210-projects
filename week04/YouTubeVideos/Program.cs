
// Main program to demonstrate the use of Video and Comment classes.

using System;
using System.Collections.Generic; // Required for List<T>

public class Program
{
    public static void Main(string[] args)
    {
        // Create a list to hold all the Video objects.
        List<Video> videos = new List<Video>();

        // --- Video 1: Programming Tutorial ---
        Video video1 = new Video("C# Basics: Understanding Classes", "CodeMaster", 900); // 15 minutes
        video1.AddComment(new Comment("Alice", "Great explanation! Very clear."));
        video1.AddComment(new Comment("Bob", "Helped me a lot with my project. Thanks!"));
        video1.AddComment(new Comment("Charlie", "Could you do a video on inheritance next?"));
        videos.Add(video1);

        // --- Video 2: Travel Vlog ---
        Video video2 = new Video("Exploring the Alps: A Summer Adventure", "Wanderlust_Explorer", 1200); // 20 minutes
        video2.AddComment(new Comment("Diana", "Amazing views! I want to go there."));
        video2.AddComment(new Comment("Eve", "What camera do you use?"));
        video2.AddComment(new Comment("Frank", "Beautiful cinematography."));
        video2.AddComment(new Comment("Grace", "Loved the music choice!"));
        videos.Add(video2);

        // --- Video 3: Cooking Recipe ---
        Video video3 = new Video("Quick & Easy Pasta Carbonara", "Chef_Gordon", 480); // 8 minutes
        video3.AddComment(new Comment("Heidi", "Tried this recipe, turned out delicious!"));
        video3.AddComment(new Comment("Ivan", "Perfect for a weeknight dinner."));
        video3.AddComment(new Comment("Judy", "Any tips for making it creamier?"));
        videos.Add(video3);

        // --- Video 4: Science Documentary ---
        Video video4 = new Video("The Wonders of the Universe", "Cosmic_Insights", 1800); // 30 minutes
        video4.AddComment(new Comment("Kevin", "Mind-blowing facts!"));
        video4.AddComment(new Comment("Laura", "Makes me feel so small."));
        video4.AddComment(new Comment("Mike", "Highly recommend this channel."));
        videos.Add(video4);


        // Iterate through the list of videos and display their information.
        Console.WriteLine("--- YouTube Video Collection ---");
        Console.WriteLine("--------------------------------");

        foreach (Video video in videos)
        {
            Console.WriteLine($"\nTitle: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLengthInSeconds()} seconds");
            // Use the GetNumberOfComments() method to display the comment count.
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

            Console.WriteLine("Comments:");
            // Iterate through the comments list for each video and display them.
            if (video.GetNumberOfComments() == 0)
            {
                Console.WriteLine("  No comments yet.");
            }
            else
            {
                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($"  - {comment.GetCommenterName()}: {comment.GetCommentText()}");
                }
            }
            Console.WriteLine("--------------------------------");
        }

        Console.WriteLine("\n--- End of Video List ---");
    }
}
// This program creates a collection of YouTube videos, each with its own comments.
// It demonstrates encapsulation by using methods to access private attributes of the Video and Comment classes.