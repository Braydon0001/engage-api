namespace Engage.Application.Extensions
{
    static public class ObjectValidationExtensions
    {
        public static T ThrowIfNull<T>(this T obj, string name)
        {
            if (obj == null)
            {
                throw new NullReferenceException(name);
            }
            return obj;
        }
    }
}
