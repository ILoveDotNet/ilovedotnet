using System.Text.RegularExpressions;

namespace OOPSDemoComponents;

public partial class EmailAddress
{
  public string Value { get; }

  private EmailAddress(string value)
  {
    Value = value;
  }

  public static EmailAddress Create(string address)
  {
    if (!EmailAddressRegex().IsMatch(address))
      throw new Exception();

    return new EmailAddress(address);
  }

  [GeneratedRegex(@"^(.+)@(.+$)")]
  private static partial Regex EmailAddressRegex();
}
