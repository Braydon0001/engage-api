namespace Engage.Application.Extensions
{
    static public class StringValidationExtension
    {
        public static string ThrowIfNullOrWhiteSpace(this string str, string name)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new NullOrWhitespaceException(name);
            }
            return str;
        }
    }
}
