namespace SharedModels;

public static class StringExtensions
{
    public static string ApplyHyphen(this string value) => value.Replace(" ", "-");
    public static string RemoveHyphen(this string value) => value.Replace("-", " ");
}