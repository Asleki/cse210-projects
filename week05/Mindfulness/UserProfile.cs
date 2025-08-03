// UserProfile.cs
using System;
using System.Collections.Generic; // Required for List<T>
using System.IO; // Required for File I/O
using System.Linq; // Required for LINQ methods like .Any()
using System.Threading; // Added for Thread.Sleep

public class UserProfile
{
    // --- Attributes ---
    private string _fullName;
    private string _username;
    private List<string> _healthConditions;
    private string _bloodType;
    private List<string> _allergies;
    private List<string> _currentMedications;
    private bool _isOrganDonor;
    private List<Address> _addresses; // A list to hold multiple Address objects

    private string _profileFilePath = "user_profile.txt"; // File to save/load profile data

    // --- Constructor ---
    public UserProfile()
    {
        _healthConditions = new List<string>();
        _allergies = new List<string>();
        _currentMedications = new List<string>();
        _addresses = new List<Address>();
        LoadProfile(); // Attempt to load profile on creation
    }

    // --- Methods for Profile Management ---

    // Loads user profile data from a text file.
    public void LoadProfile()
    {
        if (File.Exists(_profileFilePath))
        {
            try
            {
                string[] lines = File.ReadAllLines(_profileFilePath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("FullName:")) _fullName = line.Substring("FullName:".Length);
                    else if (line.StartsWith("Username:")) _username = line.Substring("Username:".Length);
                    else if (line.StartsWith("HealthConditions:")) _healthConditions = line.Substring("HealthConditions:".Length).Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                    else if (line.StartsWith("BloodType:")) _bloodType = line.Substring("BloodType:".Length);
                    else if (line.StartsWith("Allergies:")) _allergies = line.Substring("Allergies:".Length).Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                    else if (line.StartsWith("CurrentMedications:")) _currentMedications = line.Substring("CurrentMedications:".Length).Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                    else if (line.StartsWith("IsOrganDonor:")) bool.TryParse(line.Substring("IsOrganDonor:".Length), out _isOrganDonor);
                    else if (line.StartsWith("Address:"))
                    {
                        Address addr = Address.FromString(line.Substring("Address:".Length));
                        if (addr != null) _addresses.Add(addr);
                    }
                }
                Console.WriteLine(UIHelper.GetColoredText("\nℹ️ Profile loaded successfully!", ConsoleColor.Green));
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(UIHelper.GetColoredText($"❌ Error loading profile: {ex.Message}", ConsoleColor.Red));
                Thread.Sleep(2000);
            }
        }
        else
        {
            Console.WriteLine(UIHelper.GetColoredText("\nℹ️ No existing profile found. You can set one up in Settings.", ConsoleColor.Yellow));
            Thread.Sleep(2000);
        }
    }

    // Saves current user profile data to a text file.
    public void SaveProfile()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(_profileFilePath))
            {
                writer.WriteLine($"FullName:{_fullName}");
                writer.WriteLine($"Username:{_username}");
                writer.WriteLine($"HealthConditions:{string.Join(";", _healthConditions)}");
                writer.WriteLine($"BloodType:{_bloodType}");
                writer.WriteLine($"Allergies:{string.Join(";", _allergies)}");
                writer.WriteLine($"CurrentMedications:{string.Join(";", _currentMedications)}");
                writer.WriteLine($"IsOrganDonor:{_isOrganDonor}");
                foreach (Address addr in _addresses)
                {
                    writer.WriteLine($"Address:{addr.ToString()}");
                }
            }
            Console.WriteLine(UIHelper.GetColoredText("\n✅ Profile saved successfully!", ConsoleColor.Green));
            Thread.Sleep(1500);
        }
        catch (Exception ex)
        {
            Console.WriteLine(UIHelper.GetColoredText($"❌ Error saving profile: {ex.Message}", ConsoleColor.Red));
            Thread.Sleep(2000);
        }
    }

    // --- UI for Settings Menu ---
    public void DisplaySettingsMenu()
    {
        string choice = "";
        while (choice != "0")
        {
            Console.WriteLine("\n\n"); // Replaced Console.Clear() with spacing
            UIHelper.DisplayHeader("Settings");
            Console.WriteLine($"Current User: {(_username ?? "Not set")}");

            Console.WriteLine(UIHelper.GetColoredText("1. Set Full Name", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("2. Set Username", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("3. Manage Addresses", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("4. Manage Health Info", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("0. Back to Main Menu", ConsoleColor.Green));

            Console.WriteLine(UIHelper.GetColoredText("--------------------------", ConsoleColor.Yellow));
            Console.Write("Select an option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": SetFullName(); break;
                case "2": SetUsername(); break;
                case "3": ManageAddresses(); break;
                case "4": ManageHealthInfo(); break;
                case "0": SaveProfile(); Console.WriteLine("Returning to Main Menu..."); break;
                default: Console.WriteLine(UIHelper.GetColoredText("Invalid choice.", ConsoleColor.Red)); Thread.Sleep(1000); break;
            }
            if (choice != "0")
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadLine();
            }
        }
    }

    // --- Private Helper Methods for Setting Profile Details ---
    private void SetFullName()
    {
        Console.Write("Enter your full name: ");
        _fullName = Console.ReadLine();
        Console.WriteLine(UIHelper.GetColoredText("Full Name updated.", ConsoleColor.Green));
    }

    private void SetUsername()
    {
        Console.Write("Enter your username: ");
        _username = Console.ReadLine();
        Console.WriteLine(UIHelper.GetColoredText("Username updated.", ConsoleColor.Green));
    }

    private void ManageAddresses()
    {
        string choice = "";
        while (choice != "0")
        {
            Console.WriteLine("\n\n"); 
            UIHelper.DisplayHeader("Manage Addresses");
            if (_addresses.Any())
            {
                Console.WriteLine("Your current addresses:");
                for (int i = 0; i < _addresses.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_addresses[i].GetFullAddress()}");
                }
            }
            else
            {
                Console.WriteLine("No addresses added yet.");
            }

            Console.WriteLine(UIHelper.GetColoredText("\n1. Add New Address", ConsoleColor.Green));
            if (_addresses.Any())
            {
                Console.WriteLine(UIHelper.GetColoredText("2. Remove Address", ConsoleColor.Green));
            }
            Console.WriteLine(UIHelper.GetColoredText("0. Back to Settings", ConsoleColor.Green));

            Console.WriteLine(UIHelper.GetColoredText("--------------------------", ConsoleColor.Yellow));
            Console.Write("Select an option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddNewAddress(); break;
                case "2":
                    if (_addresses.Any()) RemoveAddress();
                    else Console.WriteLine(UIHelper.GetColoredText("No addresses to remove.", ConsoleColor.Red));
                    break;
                case "0": Console.WriteLine("Returning to Settings..."); break;
                default: Console.WriteLine(UIHelper.GetColoredText("Invalid choice.", ConsoleColor.Red)); break;
            }
            Thread.Sleep(1000); // Small pause for feedback
        }
    }

    private void AddNewAddress()
    {
        Console.Write("Enter Street Address: "); string street = Console.ReadLine();
        Console.Write("Enter City: "); string city = Console.ReadLine();
        Console.Write("Enter State/Province: "); string state = Console.ReadLine();
        Console.Write("Enter Zip Code: "); string zip = Console.ReadLine();
        Console.Write("Enter Country: "); string country = Console.ReadLine();
        Console.Write("Enter Address Type (e.g., Home, Work, School, Other): "); string type = Console.ReadLine();

        _addresses.Add(new Address(street, city, state, zip, country, type));
        Console.WriteLine(UIHelper.GetColoredText("Address added.", ConsoleColor.Green));
    }

    private void RemoveAddress()
    {
        Console.Write("Enter the number of the address to remove: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _addresses.Count)
        {
            _addresses.RemoveAt(index - 1);
            Console.WriteLine(UIHelper.GetColoredText("Address removed.", ConsoleColor.Green));
        }
        else
        {
            Console.WriteLine(UIHelper.GetColoredText("Invalid number.", ConsoleColor.Red));
        }
    }

    private void ManageHealthInfo()
    {
        string choice = "";
        while (choice != "0")
        {
            Console.WriteLine("\n\n"); 
            UIHelper.DisplayHeader("Health Info");
            Console.WriteLine($"Blood Type: {(_bloodType ?? "Not set")}");
            Console.WriteLine($"Organ Donor: {(_isOrganDonor ? "Yes" : "No")}");
            Console.WriteLine($"Conditions: {(_healthConditions.Any() ? string.Join(", ", _healthConditions) : "None")}");
            Console.WriteLine($"Allergies: {(_allergies.Any() ? string.Join(", ", _allergies) : "None")}");
            Console.WriteLine($"Medications: {(_currentMedications.Any() ? string.Join(", ", _currentMedications) : "None")}");

            Console.WriteLine(UIHelper.GetColoredText("\n1. Set Blood Type", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("2. Set Organ Donor Status", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("3. Add Health Condition", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("4. Remove Health Condition", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("5. Add Allergy", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("6. Remove Allergy", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("7. Add Medication", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("8. Remove Medication", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("0. Back to Settings", ConsoleColor.Green));

            Console.WriteLine(UIHelper.GetColoredText("--------------------------", ConsoleColor.Yellow));
            Console.Write("Select an option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": SetBloodType(); break;
                case "2": SetOrganDonorStatus(); break;
                case "3": AddListItem(_healthConditions, "health condition"); break;
                case "4": RemoveListItem(_healthConditions, "health condition"); break;
                case "5": AddListItem(_allergies, "allergy"); break;
                case "6": RemoveListItem(_allergies, "allergy"); break;
                case "7": AddListItem(_currentMedications, "medication"); break;
                case "8": RemoveListItem(_currentMedications, "medication"); break;
                case "0": Console.WriteLine("Returning to Settings..."); break;
                default: Console.WriteLine(UIHelper.GetColoredText("Invalid choice.", ConsoleColor.Red)); break;
            }
            Thread.Sleep(1000);
        }
    }

    private void SetBloodType()
    {
        Console.Write("Enter your Blood Type (e.g., A+, O-): ");
        _bloodType = Console.ReadLine().ToUpper();
        Console.WriteLine(UIHelper.GetColoredText("Blood Type updated.", ConsoleColor.Green));
    }

    private void SetOrganDonorStatus()
    {
        Console.Write("Are you an Organ Donor? (yes/no): ");
        string input = Console.ReadLine().ToLower();
        _isOrganDonor = (input == "yes");
        Console.WriteLine(UIHelper.GetColoredText("Organ Donor status updated.", ConsoleColor.Green));
    }

    // Generic helper for adding items to lists (conditions, allergies, medications)
    private void AddListItem(List<string> list, string itemType)
    {
        Console.Write($"Enter {itemType} to add: ");
        string item = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(item) && !list.Contains(item))
        {
            list.Add(item);
            Console.WriteLine(UIHelper.GetColoredText($"{itemType} added.", ConsoleColor.Green));
        }
        else if (list.Contains(item))
        {
            Console.WriteLine(UIHelper.GetColoredText($"{itemType} already exists.", ConsoleColor.Yellow));
        }
        else
        {
            Console.WriteLine(UIHelper.GetColoredText("Invalid input.", ConsoleColor.Red));
        }
    }

    // Generic helper for removing items from lists
    private void RemoveListItem(List<string> list, string itemType)
    {
        if (!list.Any())
        {
            Console.WriteLine(UIHelper.GetColoredText($"No {itemType}s to remove.", ConsoleColor.Yellow));
            return;
        }

        Console.WriteLine($"Current {itemType}s:");
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {list[i]}");
        }
        Console.Write($"Enter the number of the {itemType} to remove: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= list.Count)
        {
            string removedItem = list[index - 1];
            list.RemoveAt(index - 1);
            Console.WriteLine(UIHelper.GetColoredText($"{removedItem} removed.", ConsoleColor.Green));
        }
        else
        {
            Console.WriteLine(UIHelper.GetColoredText("Invalid number.", ConsoleColor.Red));
        }
    }

    // --- Getters for MoodCheckIn to use ---
    public string GetUsername() { return _username; }
    public List<Address> GetAddressesByType(string type)
    {
        return _addresses.Where(a => a.GetAddressType().Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}