namespace Engage.Application.Utils;

static public class StringUtils
{
    public static string RemoveDCProductCodeSuffix(string code)
    {
        if (!string.IsNullOrWhiteSpace(code))
        {
            var length = code.IndexOfAny(new char[] { ',', '.' });
            if (length >= 0)
            {
                return code[..length];
            }
        }
        return code;
    }

    public static string GetFileName(string url)
    {
        var slashIndex = url.LastIndexOf("/");
        var dotIndex = url.LastIndexOf(".");
        
        if (slashIndex >= 0 && slashIndex < url.Length && dotIndex >= 0 && dotIndex < url.Length)
        {
            return url.Substring(slashIndex + 1, dotIndex - slashIndex);
        }
        return string.Empty;
    }
    
    public static string GetFileExtension(string fileName)
    {
        var indexOf = fileName.LastIndexOf(".");

        if (indexOf >= 0 && indexOf <= fileName.Length)
        {
            return fileName[(indexOf + 1)..];
        }
        return string.Empty;
    }
}
