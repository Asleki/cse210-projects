using System;
using System.Collections.Generic;
using System.IO; // Required for file operations
using System.Text.Json; // Required for JSON serialization

// Represents the user's profile, storing personal, health, and address information.
// This is an exceeding requirement feature, providing personalized data management.
public class UserProfile
{
    // Properties for personal information.
    public string FullName { get; set; } = "Not Set";
    public string Username { get; set; } = "Guest";

    // Properties for health information.
    public string BloodType { get; set; } = "Not Set";
    public bool IsOrganDonor { get; set; } = false;
    public List<string> HealthConditions { get; set; } = new List<string>();
    public List<string> Allergies { get; set; } = new List<string>();
    public List<string> CurrentMedications { get; set; } = new List<string>();

    // List to store multiple addresses for the user.
    public List<Address> Addresses { get; set; } = new List<Address>();

    // Constructor initializes lists to prevent null reference exceptions.
    public UserProfile()
    {
        // Lists are initialized in property definitions for conciseness.
    }

    // Displays the main settings menu and handles user choices within it.
    // This is part of the exceeding requirements for settings management.
    public void DisplaySettingsMenu()
    {
        string choice = "";
        while (choice != "0")
        {
            UIHelper.PrintHeader("DigiHealth", "Settings");
            Console.WriteLine("Manage your profile:");
            UIHelper.PrintColor("  1. ", ConsoleColor.Green); Console.WriteLine("View Profile");
            UIHelper.PrintColor("  2. ", ConsoleColor.Green); Console.WriteLine("Edit Personal Info");
            UIHelper.PrintColor("  3. ", ConsoleColor.Green); Console.WriteLine("Edit Health Info");
            UIHelper.PrintColor("  4. ", ConsoleColor.Green); Console.WriteLine("Manage Addresses");
            UIHelper.PrintColor("  0. ", ConsoleColor.Green); Console.WriteLine("Back to Main Menu");
            Console.WriteLine();

            UIHelper.PrintColor("Enter your choice: ", ConsoleColor.Yellow);
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ViewProfile(); break;
                case "2": EditPersonalInfo(); break;
                case "3": EditHealthInfo(); break;
                case "4": ManageAddresses(); break;
                case "0": break; // Exit loop
                default: UIHelper.PrintColor("Invalid choice. Please enter a number from the menu.\n", ConsoleColor.Red); break;
            }

            if (choice != "0")
            {
                UIHelper.PrintColor("\nPress Enter to continue...", ConsoleColor.Yellow);
                Console.ReadLine();
            }
        }
        SaveProfile(); // Save profile changes when exiting settings
    }

    // Displays all current profile information.
    private void ViewProfile()
    {
        UIHelper.PrintHeader("Settings", "View Profile");
        UIHelper.PrintColor("--- Personal Information ---\n", ConsoleColor.Cyan);
        Console.WriteLine($"Full Name: {FullName}");
        Console.WriteLine($"Username: {Username}");
        Console.WriteLine();

        UIHelper.PrintColor("--- Health Information ---\n", ConsoleColor.Cyan);
        Console.WriteLine($"Blood Type: {BloodType}");
        Console.WriteLine($"Organ Donor: {(IsOrganDonor ? "Yes" : "No")}");
        Console.WriteLine($"Health Conditions: {(HealthConditions.Count == 0 ? "None" : string.Join(", ", HealthConditions))}");
        Console.WriteLine($"Allergies: {(Allergies.Count == 0 ? "None" : string.Join(", ", Allergies))}");
        Console.WriteLine($"Current Medications: {(CurrentMedications.Count == 0 ? "None" : string.Join(", ", CurrentMedications))}");
        Console.WriteLine();

        UIHelper.PrintColor("--- Addresses ---\n", ConsoleColor.Cyan);
        if (Addresses.Count == 0)
        {
            Console.WriteLine("No addresses saved.");
        }
        else
        {
            for (int i = 0; i < Addresses.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {Addresses[i].ToString()}");
            }
        }
    }

    // Allows the user to edit their personal information.
    private void EditPersonalInfo()
    {
        UIHelper.PrintHeader("Settings", "Edit Personal Info");
        UIHelper.PrintColor($"Current Full Name: {FullName}\n", ConsoleColor.White);
        UIHelper.PrintColor("Enter new Full Name (or press Enter to keep current): ", ConsoleColor.Yellow);
        string newFullName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newFullName))
        {
            FullName = newFullName;
            UIHelper.PrintColor("Full Name updated.", ConsoleColor.Green);
        }

        UIHelper.PrintColor($"Current Username: {Username}\n", ConsoleColor.White);
        UIHelper.PrintColor("Enter new Username (or press Enter to keep current): ", ConsoleColor.Yellow);
        string newUsername = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newUsername))
        {
            Username = newUsername;
            UIHelper.PrintColor("Username updated.", ConsoleColor.Green);
        }
        SaveProfile(); // Save changes immediately
    }

    // Allows the user to edit their health information.
    private void EditHealthInfo()
    {
        UIHelper.PrintHeader("Settings", "Edit Health Info");

        UIHelper.PrintColor($"Current Blood Type: {BloodType}\n", ConsoleColor.White);
        UIHelper.PrintColor("Enter new Blood Type (e.g., A+, O-, or press Enter to keep current): ", ConsoleColor.Yellow);
        string newBloodType = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newBloodType))
        {
            BloodType = newBloodType.ToUpper(); // Standardize to uppercase
            UIHelper.PrintColor("Blood Type updated.", ConsoleColor.Green);
        }

        UIHelper.PrintColor($"Current Organ Donor Status: {(IsOrganDonor ? "Yes" : "No")}\n", ConsoleColor.White);
        UIHelper.PrintColor("Are you an Organ Donor? (Y/N, or press Enter to keep current): ", ConsoleColor.Yellow);
        string newIsOrganDonor = Console.ReadLine()?.ToLower();
        if (newIsOrganDonor == "y")
        {
            IsOrganDonor = true;
            UIHelper.PrintColor("Organ Donor status updated to Yes.", ConsoleColor.Green);
        }
        else if (newIsOrganDonor == "n")
        {
            IsOrganDonor = false;
            UIHelper.PrintColor("Organ Donor status updated to No.", ConsoleColor.Green);
        }

        HealthConditions = EditStringList("Health Conditions", HealthConditions);
        Allergies = EditStringList("Allergies", Allergies);
        CurrentMedications = EditStringList("Current Medications", CurrentMedications);

        SaveProfile(); // Save changes immediately
    }

    // Helper method to edit lists of strings (e.g., health conditions, allergies).
    private List<string> EditStringList(string listName, List<string> currentList)
    {
        UIHelper.PrintColor($"\n--- Edit {listName} ---\n", ConsoleColor.Cyan);
        Console.WriteLine($"Current {listName}: {(currentList.Count == 0 ? "None" : string.Join(", ", currentList))}");
        UIHelper.PrintColor($"Enter new {listName} (comma-separated, or press Enter to keep current): ", ConsoleColor.Yellow);
        string input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
        {
            // Split by comma, remove empty entries, trim whitespace from each.
            List<string> newList = new List<string>(input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList());
            UIHelper.PrintColor($"{listName} updated.", ConsoleColor.Green);
            return newList;
        }
        return currentList; // Return original list if input is empty
    }

    // Manages adding and removing addresses from the user's profile.
    private void ManageAddresses()
    {
        string choice = "";
        while (choice != "0")
        {
            UIHelper.PrintHeader("Settings", "Manage Addresses");
            if (Addresses.Count == 0)
            {
                Console.WriteLine("No addresses saved.");
            }
            else
            {
                UIHelper.PrintColor("Your Saved Addresses:\n", ConsoleColor.Cyan);
                for (int i = 0; i < Addresses.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {Addresses[i].ToString()}");
                }
            }
            Console.WriteLine("\nOptions:");
            UIHelper.PrintColor("  1. ", ConsoleColor.Green); Console.WriteLine("Add New Address");
            UIHelper.PrintColor("  2. ", ConsoleColor.Green); Console.WriteLine("Remove Address");
            UIHelper.PrintColor("  0. ", ConsoleColor.Green); Console.WriteLine("Back to Settings Menu");
            Console.WriteLine();

            UIHelper.PrintColor("Enter your choice: ", ConsoleColor.Yellow);
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddAddress(); break;
                case "2": RemoveAddress(); break;
                case "0": break; // Exit loop
                default: UIHelper.PrintColor("Invalid choice. Please enter a number from the menu.\n", ConsoleColor.Red); break;
            }

            if (choice != "0")
            {
                UIHelper.PrintColor("\nPress Enter to continue...", ConsoleColor.Yellow);
                Console.ReadLine();
            }
        }
        SaveProfile(); // Save addresses changes when exiting management
    }

    // Guides the user through adding a new address to their profile.
    private void AddAddress()
    {
        UIHelper.PrintHeader("Addresses", "Add New Address");
        UIHelper.PrintColor("Enter Address Type (e.g., Home, Work, School, Other): ", ConsoleColor.Yellow);
        string type = Console.ReadLine()?.Trim() ?? "Other";
        UIHelper.PrintColor("Enter Street: ", ConsoleColor.Yellow);
        string street = Console.ReadLine()?.Trim() ?? "";
        UIHelper.PrintColor("Enter City: ", ConsoleColor.Yellow);
        string city = Console.ReadLine()?.Trim() ?? "";
        UIHelper.PrintColor("Enter State/Province: ", ConsoleColor.Yellow);
        string state = Console.ReadLine()?.Trim() ?? "";
        UIHelper.PrintColor("Enter Zip/Postal Code: ", ConsoleColor.Yellow);
        string zip = Console.ReadLine()?.Trim() ?? "";
        UIHelper.PrintColor("Enter Country: ", ConsoleColor.Yellow);
        string country = Console.ReadLine()?.Trim() ?? "";

        Addresses.Add(new Address(type, street, city, state, zip, country));
        UIHelper.PrintColor("\nAddress added successfully!", ConsoleColor.Green);
        SaveProfile(); // Save changes immediately
    }

    // Allows the user to remove an existing address from their profile.
    private void RemoveAddress()
    {
        UIHelper.PrintHeader("Addresses", "Remove Address");
        if (Addresses.Count == 0)
        {
            Console.WriteLine("No addresses to remove.");
            return;
        }

        UIHelper.PrintColor("Select the number of the address to remove:\n", ConsoleColor.Yellow);
        for (int i = 0; i < Addresses.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {Addresses[i].ToString()}");
        }

        int choice;
        bool isValidChoice = false;
        do
        {
            UIHelper.PrintColor($"Enter choice (1-{Addresses.Count}): ", ConsoleColor.Yellow);
            string input = Console.ReadLine();
            isValidChoice = int.TryParse(input, out choice) && choice > 0 && choice <= Addresses.Count;

            if (!isValidChoice)
            {
                UIHelper.PrintColor("Invalid choice. Please enter a valid number.\n", ConsoleColor.Red);
            }
        } while (!isValidChoice);

        string removedAddress = Addresses[choice - 1].ToString();
        Addresses.RemoveAt(choice - 1);
        UIHelper.PrintColor($"\nAddress '{removedAddress}' removed.", ConsoleColor.Green);
        SaveProfile(); // Save changes immediately
    }

    // Loads user profile data from a JSON file (user_profile.json).
    // This supports persistent user settings, an exceeding requirement.
    public void LoadProfile()
    {
        string filePath = "user_profile.json";
        if (File.Exists(filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                UserProfile loadedProfile = JsonSerializer.Deserialize<UserProfile>(jsonString);
                // Transfer loaded data to the current instance
                if (loadedProfile != null)
                {
                    FullName = loadedProfile.FullName;
                    Username = loadedProfile.Username;
                    BloodType = loadedProfile.BloodType;
                    IsOrganDonor = loadedProfile.IsOrganDonor;
                    HealthConditions = loadedProfile.HealthConditions ?? new List<string>();
                    Allergies = loadedProfile.Allergies ?? new List<string>();
                    CurrentMedications = loadedProfile.CurrentMedications ?? new List<string>();
                    Addresses = loadedProfile.Addresses ?? new List<Address>();
                }
                // Console.WriteLine("Profile loaded successfully."); // For debug
            }
            catch (Exception ex)
            {
                // If loading fails (e.g., malformed JSON), log error and start with default profile.
                UIHelper.PrintColor($"Error loading profile: {ex.Message}\nStarting with default profile.", ConsoleColor.Red);
            }
        }
        // else, profile will remain at default initialized values.
    }

    // Saves current user profile data to a JSON file (user_profile.json).
    // This supports persistent user settings, an exceeding requirement.
    public void SaveProfile()
    {
        string filePath = "user_profile.json";
        try
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(filePath, jsonString);
            // Console.WriteLine("Profile saved successfully."); // For debug
        }
        catch (Exception ex)
        {
            UIHelper.PrintColor($"Error saving profile: {ex.Message}", ConsoleColor.Red);
        }
    }
}