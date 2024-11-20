using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Engage.Application.Extensions;

public static class ArgumentExtensions
{
    public static string NotNullOrWhiteSpace([NotNull] this string value, [CallerArgumentExpression("value")] string name = "")
    {
        return string.IsNullOrWhiteSpace(value)
                 ? throw new ArgumentException(name)
                 : value;
    }
    public static int GreaterThanZero(this int value, [CallerArgumentExpression("value")] string name = "")
    {
        return value <= 0
            ? throw new ArgumentOutOfRangeException(name, $"The argument must be greater than zero.")
            : value;
    }
}
