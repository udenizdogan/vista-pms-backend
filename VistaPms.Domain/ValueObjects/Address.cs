namespace VistaPms.Domain.ValueObjects;

public class Address
{
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string? PostalCode { get; init; }

    public Address(string street, string city, string country, string? postalCode = null)
    {
        Street = street;
        City = city;
        Country = country;
        PostalCode = postalCode;
    }

    protected Address() { }

    public string FullAddress => $"{Street}, {City}, {Country}" + (PostalCode != null ? $" {PostalCode}" : "");
}
