
// Represents a single comment on a YouTube video.

using System;

public class Comment
{
    // Private attributes to encapsulate the comment's data.
    // These cannot be directly accessed or modified from outside this class.
    private string _commenterName;
    private string _commentText;

    // Constructor for the Comment class.
    // It initializes a new Comment object with the provided name and text.
    public Comment(string commenterName, string commentText)
    {
        _commenterName = commenterName;
        _commentText = commentText;
    }

    // Public method to get the name of the person who made the comment.
    // This provides controlled access to the private _commenterName attribute.
    public string GetCommenterName()
    {
        return _commenterName;
    }

    // Public method to get the text content of the comment.
    // This provides controlled access to the private _commentText attribute.
    public string GetCommentText()
    {
        return _commentText;
    }
}
