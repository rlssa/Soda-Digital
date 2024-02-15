using System.ComponentModel;
using System.Reflection;

namespace RoyalLifeSavings.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this System.Enum value)
    {
        if (value is null) return string.Empty;
        return value
                   .GetType()
                   .GetMember(value.ToString())
                   .FirstOrDefault()
                   ?.GetCustomAttribute<DescriptionAttribute>()
                   ?.Description
               ?? value.ToString();
    }
}
