namespace VistaPms.Domain.ValueObjects;

public class PhoneNumber
{
    public string CountryCode { get; init; } = string.Empty;
    public string Number { get; init; } = string.Empty;

    public PhoneNumber(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
    }

    protected PhoneNumber() { }

    public string FullNumber => $"{CountryCode}{Number}";

    public override string ToString() => FullNumber;
}
