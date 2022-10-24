namespace SharedModels;
public static class SharedExtensions
{
    public static IEnumerable<(T, int)> SelectWithIndex<T>(this IEnumerable<T> items) => items.Select((item, index) => (item, index));
}
