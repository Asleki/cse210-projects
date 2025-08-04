using System;
using System.Text.Json.Serialization; // Required for JsonPropertyName

// Represents a single address entry within the UserProfile.
// This supports the comprehensive settings management exceeding requirement.
public class Address
{
    // Properties for address components.
    // JsonPropertyName attributes ensure consistent serialization/deserialization.
    [JsonPropertyName("addressType")]
    public string AddressType { get; set; }

    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("zip")]
    public string Zip { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    // Default constructor for JSON deserialization.
    public Address() 
    {
        // Initialize string properties to non-null defaults to avoid NRE.
        AddressType = ""; 
        Street = "";
        City = "";
        State = "";
        Zip = "";
        Country = "";
    }

    // Parameterized constructor for creating new Address instances.
    public Address(string addressType, string street, string city, string state, string zip, string country)
    {
        AddressType = addressType;
        Street = street;
        City = city;
        State = state;
        Zip = zip;
        Country = country;
    }

    // Overrides ToString() to provide a formatted string representation of the address.
    public override string ToString()
    {
        string addressPart1 = string.IsNullOrEmpty(Street) ? "" : $"{Street}, ";
        string addressPart2 = string.IsNullOrEmpty(City) ? "" : $"{City}";
        string addressPart3 = string.IsNullOrEmpty(State) ? "" : $", {State}";
        string addressPart4 = string.IsNullOrEmpty(Zip) ? "" : $" {Zip}";
        string addressPart5 = string.IsNullOrEmpty(Country) ? "" : $", {Country}";

        string fullAddress = $"{AddressType}: {addressPart1}{addressPart2}{addressPart3}{addressPart4}{addressPart5}".Trim();
        
        // Remove trailing comma if it exists after trimming
        if (fullAddress.EndsWith(","))
        {
            fullAddress = fullAddress.TrimEnd(',');
        }

        return fullAddress;
    }
}