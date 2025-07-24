
// Represents a YouTube video and manages its details and comments.

using System;
using System.Collections.Generic; // Required for List<T>

public class Video
{
    // Private attributes to encapsulate the video's data.
    // These cannot be directly accessed or modified from outside this class.
    private string _title;
    private string _author;
    private int _lengthInSeconds; // Length of the video in seconds
    private List<Comment> _comments; // A list to store Comment objects associated with this video

    // Constructor for the Video class.
    // It initializes a new Video object with its title, author, and length.
    // It also initializes the list of comments as an empty list.
    public Video(string title, string author, int lengthInSeconds)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>(); // Initialize the list to prevent null reference exceptions
    }

    // Public method to add a new comment to this video's list of comments.
    // This method encapsulates the process of adding a comment.
    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    // Public method to get the number of comments on this video.
    // This method provides controlled access to the internal _comments list's count.
    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    // Public method to get the title of the video.
    public string GetTitle()
    {
        return _title;
    }

    // Public method to get the author of the video.
    public string GetAuthor()
    {
        return _author;
    }

    // Public method to get the length of the video in seconds.
    public int GetLengthInSeconds()
    {
        return _lengthInSeconds;
    }

    // Public method to get the list of comments.
    // While this exposes the list, it's often done for iteration purposes.
    // For strict encapsulation, one might provide a method to get comments by index,
    // or iterate internally and return formatted strings. For this assignment,
    // returning the list directly for iteration is acceptable.
    public List<Comment> GetComments()
    {
        return _comments;
    }
}
// This class represents a YouTube video, encapsulating its title, author, length, and comments.
// It provides methods to add comments and retrieve video details, ensuring encapsulation of its attributes.