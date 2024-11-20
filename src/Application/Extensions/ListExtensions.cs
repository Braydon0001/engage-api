namespace Engage.Application.Extensions;

static public class ListExtensions
{
    public static List<T> CheckNullOrEmpty<T>(this List<T> list, string name)
    {
        if (list == null)
        {
            throw new NullReferenceException(name);
        }
        if (list.Count == 0)
        {
            throw new EmptyListException(name);
        }
        return list;
    }

    public static bool NotNullOrEmpty<T>(this List<T> list)
    {
        return (list != null && list.Count > 0);
    }
}
