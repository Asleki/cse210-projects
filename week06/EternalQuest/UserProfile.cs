// UserProfile.cs
//
// This file defines the UserProfile class, which encapsulates all
// of the user's personal information and health conditions.
// It includes methods for converting the data to and from a string
// format for saving and loading.

public class UserProfile
{
    // Private member variables to hold the user's data
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string HomeAddress { get; private set; }
    public string WorkAddress { get; private set; }
    public string ChurchAddress { get; private set; }
    public string HealthConditions { get; private set; }

    // Constructor to initialize a new UserProfile object
    public UserProfile(string username, string email, string homeAddress,
                       string workAddress, string churchAddress, string healthConditions)
    {
        Username = username;
        Email = email;
        HomeAddress = homeAddress;
        WorkAddress = workAddress;
        ChurchAddress = churchAddress;
        HealthConditions = healthConditions;
    }

    // A method to create a string representation of the profile for saving to a file
    public string GetStringRepresentation()
    {
        return $"Username: {Username}\n" +
               $"Email: {Email}\n" +
               $"Home Address: {HomeAddress}\n" +
               $"Work Address: {WorkAddress}\n" +
               $"Church Address: {ChurchAddress}\n" +
               $"Health Conditions: {HealthConditions}";
    }

    // A static method to create a UserProfile object from a string array,
    // which would be used when loading from a file.
    public static UserProfile LoadFromFileLines(string[] lines)
    {
        string username = "";
        string email = "";
        string homeAddress = "";
        string workAddress = "";
        string churchAddress = "";
        string healthConditions = "";

        foreach (string line in lines)
        {
            if (line.StartsWith("Username:"))
            {
                username = line.Substring("Username: ".Length);
            }
            else if (line.StartsWith("Email:"))
            {
                email = line.Substring("Email: ".Length);
            }
            else if (line.StartsWith("Home Address:"))
            {
                homeAddress = line.Substring("Home Address: ".Length);
            }
            else if (line.StartsWith("Work Address:"))
            {
                workAddress = line.Substring("Work Address: ".Length);
            }
            else if (line.StartsWith("Church Address:"))
            {
                churchAddress = line.Substring("Church Address: ".Length);
            }
            else if (line.StartsWith("Health Conditions:"))
            {
                healthConditions = line.Substring("Health Conditions: ".Length);
            }
        }

        return new UserProfile(username, email, homeAddress,
                               workAddress, churchAddress, healthConditions);
    }
}
