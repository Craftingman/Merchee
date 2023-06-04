using System.Security.Cryptography;

public static class StringHelper
{
    public static string GetRandomlyGenerateBase64String(int count)
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(count));
    }
}