using System.Security.Cryptography;
using System.Text;

namespace JobsFinder.Application.Services.Cryptography;
public class PasswordEncripter
{
    private readonly string _additionalKey;
    public PasswordEncripter(string additionalKey) => _additionalKey = additionalKey;
    public string Encript(string password)
    {
        var newPassword = $"{password}{_additionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hasBytes = SHA512.HashData(bytes);

        return StringBytes(hasBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
