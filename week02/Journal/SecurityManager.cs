using System;
using System.IO; // Required for file operations (reading/writing privacy.txt)
using System.Reflection; // Required for Assembly.GetExecutingAssembly().Location if used, but AppDomain.CurrentDomain.BaseDirectory is often sufficient

// This class handles all security-related features, such as setting,
// verifying, and storing user passwords or PINs.
public class SecurityManager
{
    // The name of the file where the password/PIN will be stored.
    // This file should NOT be committed to public repositories like GitHub.
    // We now construct the path to ensure it's always relative to the executable's directory.
    private string _privacyFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "privacy.txt");

    // Stores the password/PIN loaded from the file.
    private string _storedPassword = "";

    // Constructor: When a SecurityManager object is created, it tries to load
    // any existing password from the privacy file.
    public SecurityManager()
    {
        LoadStoredPassword();
    }

    // Checks if a password/PIN has already been set and stored.
    public bool IsPasswordSet()
    {
        // A password is considered set if the file exists and contains content.
        return File.Exists(_privacyFilename) && !string.IsNullOrWhiteSpace(_storedPassword);
    }

    // Loads the password/PIN from the privacy file into memory.
    private void LoadStoredPassword()
    {
        try
        {
            if (File.Exists(_privacyFilename))
            {
                // Reads the entire content of the file (expecting only one line: the password/PIN).
                _storedPassword = File.ReadAllText(_privacyFilename).Trim();
            }
            else
            {
                _storedPassword = ""; // No password if file doesn't exist
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading stored password: {ex.Message}");
            _storedPassword = ""; // Ensure it's empty on error
        }
    }

    // Allows the user to set a new password or PIN.
    public void SetNewSecurity()
    {
        Console.WriteLine("\n--- Set Your Privacy Feature ---");
        Console.WriteLine("Available Security Methods:");
        Console.WriteLine("1. PIN (Digits only)");
        Console.WriteLine("2. Password (Digits, special characters, letters)");
        Console.Write("Choose your method (1 or 2): ");
        string methodChoice = Console.ReadLine();

        string newPassword = "";
        bool isValid = false;

        while (!isValid)
        {
            Console.Write("Enter your new security code: ");
            newPassword = Console.ReadLine();
            Console.Write("Repeat to verify: ");
            string verifyPassword = Console.ReadLine();

            if (newPassword == verifyPassword)
            {
                // Basic validation based on method choice
                if (methodChoice == "1") // PIN
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"^\d+$"))
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("PIN must contain only digits. Please try again.");
                    }
                }
                else if (methodChoice == "2") // Password
                {
                    // A simple check for mixed characters. Can be made more robust.
                    if (newPassword.Length >= 6 &&
                        System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"[a-zA-Z]") &&
                        System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"\d") &&
                        System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-`~]"))
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Password must be at least 6 characters and include letters, digits, and special characters. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid method choice. Please enter 1 or 2.");
                }
            }
            else
            {
                Console.WriteLine("Codes do not match. Please try again.");
            }
        }

        try
        {
            // Save the new password to the file.
            File.WriteAllText(_privacyFilename, newPassword);
            _storedPassword = newPassword; // Update in-memory copy
            Console.WriteLine("Security feature set successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving new security code: {ex.Message}");
            Console.WriteLine("Failed to set security feature. Please check file permissions.");
        }
    }

    // Verifies if the user-entered password matches the stored password.
    public bool VerifyAccess()
    {
        // If no password is set, access is automatically granted.
        if (!IsPasswordSet())
        {
            return true;
        }

        Console.Write("Enter your security code to proceed: ");
        string enteredPassword = Console.ReadLine();

        if (enteredPassword == _storedPassword)
        {
            Console.WriteLine("Access granted.");
            return true;
        }
        else
        {
            Console.WriteLine("Access denied. Incorrect security code.");
            return false;
        }
    }
}
