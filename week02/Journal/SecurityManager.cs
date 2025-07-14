using System; // Provides fundamental classes and base types, including Console for input/output.
using System.IO; // Necessary for file operations (reading and writing files) and path manipulation.

// This class is responsible for managing all security-related features of the application.
// Its primary functions include setting up a privacy feature (password or PIN),
// verifying user access, and persistently storing user security settings along with a username.
public class SecurityManager
{
    // This field stores the full, absolute path to the file where the security settings
    // (username and password/PIN) are persistently saved. This file is intended to be
    // private and should not be shared or committed to public repositories.
    private string _securityFilePath;

    // This field holds the user's password or PIN once it's loaded from the security file.
    private string _storedPassword = "";
    // This field holds the user's chosen username, which is also loaded from the security file.
    // It defaults to "Guest" if no username is set or loaded.
    private string _username = "Guest";

    // The constructor for the SecurityManager class.
    // When a new SecurityManager object is created, this code automatically runs
    // to determine the correct path for the security file and attempts to load
    // any existing security settings from that file.
    public SecurityManager()
    {
        // Calculates the path to the project's root directory.
        // AppDomain.CurrentDomain.BaseDirectory points to the executable's location (e.g., bin/Debug/net8.0/).
        // Path.Combine with ".." is used to navigate up three directory levels to reach the project root.
        string projectRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        _securityFilePath = Path.Combine(projectRootPath, "privacy.txt"); // Sets the full path for the security file.

        // Automatically attempts to load the username and stored password/PIN from the security file.
        LoadSecuritySettings();
    }

    // This method checks if a privacy feature (password/PIN) has already been successfully set and stored.
    // A password is considered set if the security file exists, contains a non-empty password,
    // and a non-empty, non-default username is also present.
    public bool IsPasswordSet()
    {
        return File.Exists(_securityFilePath) && !string.IsNullOrWhiteSpace(_storedPassword) && !string.IsNullOrWhiteSpace(_username) && _username != "Guest";
    }

    // This method loads the username and password/PIN from the security file into memory.
    // It parses the file content, expecting specific "Username:" and "Password:" lines.
    private void LoadSecuritySettings()
    {
        // Resets the username and password to their default/empty states before attempting to load new values.
        _username = "Guest";
        _storedPassword = "";

        try
        {
            // Checks if the security file exists before attempting to read it.
            if (File.Exists(_securityFilePath))
            {
                // Reads all lines from the security file.
                string[] lines = File.ReadAllLines(_securityFilePath);
                // Iterates through each line to find and extract the username and password.
                foreach (string line in lines)
                {
                    // If a line starts with "Username:", the text after the label is extracted as the username.
                    if (line.StartsWith("Username:", StringComparison.OrdinalIgnoreCase))
                    {
                        _username = line.Substring("Username:".Length).Trim();
                    }
                    // If a line starts with "Password:", the text after the label is extracted as the password.
                    else if (line.StartsWith("Password:", StringComparison.OrdinalIgnoreCase))
                    {
                        _storedPassword = line.Substring("Password:".Length).Trim();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Catches any errors that occur during file loading (e.g., file corruption, permission issues).
            // Resets username and password to ensure a safe state if loading fails.
            Console.WriteLine($"Error loading security settings: {ex.Message}");
            _username = "Guest";
            _storedPassword = "";
        }
    }

    // This method guides the user through the process of setting up a new privacy feature.
    // It prompts for a username and then allows the user to choose between a PIN or a password,
    // enforcing basic validation rules for the chosen method.
    public void SetNewSecurity()
    {
        Console.WriteLine("\n--- Set Your Privacy Feature ---");
        Console.Write("Enter your desired username: ");
        string newUsername = Console.ReadLine().Trim();
        // If the entered username is empty, it defaults to "Guest" to ensure a valid username is always set.
        if (string.IsNullOrWhiteSpace(newUsername))
        {
            Console.WriteLine("Username cannot be empty. Setting as 'Guest'.");
            newUsername = "Guest";
        }

        Console.WriteLine("Available Security Methods:");
        Console.WriteLine("1. PIN (Digits only)");
        Console.WriteLine("2. Password (Digits, special characters, letters)");
        Console.Write("Choose your method (1 or 2): ");
        string methodChoice = Console.ReadLine();

        string newPassword = "";
        bool isValid = false;

        // A loop that continues until the user provides a valid security code that matches on verification.
        while (!isValid)
        {
            Console.Write("Enter your new security code: ");
            newPassword = Console.ReadLine();
            Console.Write("Repeat to verify: ");
            string verifyPassword = Console.ReadLine();

            // Checks if the entered security code matches the verification code.
            if (newPassword == verifyPassword)
            {
                // Applies validation rules based on the user's chosen security method.
                if (methodChoice == "1") // PIN method validation.
                {
                    // Checks if the PIN consists only of digits.
                    if (System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"^\d+$"))
                    {
                        isValid = true; // PIN is valid.
                    }
                    else
                    {
                        Console.WriteLine("PIN must contain only digits. Please try again.");
                    }
                }
                else if (methodChoice == "2") // Password method validation.
                {
                    // Checks for minimum length and presence of letters, digits, and special characters.
                    if (newPassword.Length >= 6 &&
                        System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"[a-zA-Z]") &&
                        System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"\d") &&
                        System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-`~]"))
                    {
                        isValid = true; // Password is valid.
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
                Console.WriteLine("Security codes do not match. Please try again.");
            }
        }

        try
        {
            // Formats the username and new password into a string to be saved in the security file.
            string contentToSave = $"Username: {newUsername}\nPassword: {newPassword}";
            // Writes the formatted security content to the privacy.txt file, overwriting any existing content.
            File.WriteAllText(_securityFilePath, contentToSave);
            // Updates the in-memory copies of the username and stored password with the newly set values.
            _username = newUsername;
            _storedPassword = newPassword;
            Console.WriteLine("Security feature set successfully!"); // Confirms successful setup to the user.
        }
        catch (Exception ex)
        {
            // Catches any errors that occur during saving the security code (e.g., file permission issues).
            // An informative error message is displayed to the user.
            Console.WriteLine($"Error saving new security code: {ex.Message}");
            Console.WriteLine("Failed to set security feature. Please check file permissions.");
        }
    }

    // This method verifies if the user-entered security code matches the stored password/PIN.
    public bool VerifyAccess()
    {
        // If no password is set (e.g., for a new user who skipped setup), access is automatically granted.
        if (!IsPasswordSet())
        {
            return true;
        }

        Console.Write("Enter your security code to proceed: ");
        string enteredPassword = Console.ReadLine();

        // Compares the entered password with the stored password.
        if (enteredPassword == _storedPassword)
        {
            Console.WriteLine("Access granted."); // Confirms successful access.
            return true;
        }
        else
        {
            Console.WriteLine("Access denied. Incorrect security code."); // Informs the user of incorrect input.
            return false;
        }
    }

    // This method provides the stored username to other parts of the application.
    // It allows for personalized greetings and displays throughout the program.
    public string GetUsername()
    {
        return _username;
    }
}
