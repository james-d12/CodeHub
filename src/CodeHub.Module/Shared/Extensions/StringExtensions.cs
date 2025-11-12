namespace CodeHub.Module.Shared.Extensions;

public static class StringExtensions
{
    public static bool EqualsCaseInsensitive(this string s, string? value)
    {
        return string.Equals(value, s, StringComparison.OrdinalIgnoreCase);
    }

    public static bool ContainsCaseInsensitive(this string s, string? value)
    {
        return s.Contains(value ?? string.Empty, StringComparison.OrdinalIgnoreCase);
    }
}