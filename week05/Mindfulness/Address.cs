// Address.cs
using System;

public class Address
{
    // --- Attributes ---
    private string _streetAddress;
    private string _city;
    private string _stateProvince; 
    private string _zipCode;
    private string _country;
    private string _addressType; // e.g., "Home", "Work", "School", "Other"

    // --- Constructor ---
    public Address(string streetAddress, string city, string stateProvince, string zipCode, string country, string addressType)
    {
        _streetAddress = streetAddress;
        _city = city;
        _stateProvince = stateProvince;
        _zipCode = zipCode;
        _country = country;
        _addressType = addressType;
    }

    // --- Getters ---
    public string GetStreetAddress() { return _streetAddress; }
    public string GetCity() { return _city; }
    public string GetStateProvince() { return _stateProvince; }
    public string GetZipCode() { return _zipCode; }
    public string GetCountry() { return _country; }
    public string GetAddressType() { return _addressType; }

    // --- Method to display the address ---
    public string GetFullAddress()
    {
        return $"{_streetAddress}, {_city}, {_stateProvince} {_zipCode}, {_country} ({_addressType})";
    }

    // --- Method to convert to string for saving/displaying simpler form ---
    // This format will be used for saving and loading from file
    public override string ToString()
    {
        return $"{_streetAddress}|{_city}|{_stateProvince}|{_zipCode}|{_country}|{_addressType}";
    }

    // --- Static method to parse an Address object from a string ---
    public static Address FromString(string addressString)
    {
        string[] parts = addressString.Split('|');
        if (parts.Length == 6)
        {
            return new Address(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
        }
        return null; // Return null if format is incorrect
    }
}