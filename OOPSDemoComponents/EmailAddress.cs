using System.Text.RegularExpressions;

namespace OOPSDemoComponents;

public class EmailAddress
{
    public string Value { get; }

    private EmailAddress(string value)
    {
        Value = value;
    }

    public static EmailAddress Create(string address)
    {
        if (!Regex.IsMatch(address, @"^(.+)@(.+$)"))
            throw new Exception();

        return new EmailAddress(address);
    }
}
