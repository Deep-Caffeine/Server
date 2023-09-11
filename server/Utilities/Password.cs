using System.Security.Cryptography;
using System.Text;

namespace server.Utilities;

public static class Password
{
    public static string GenerateRandomPassword(int length = 15)
    {
        Random random = new Random();
        string items = "ABCDEFGHIJKLNMOPQRSTUVWXYZabcdefghijklnmopqrstuvwxyz0123456789!@#$%^&*()_+=[]{}:;\'\",./<>?\\";

        char[] password = new char[length];

        for (int i = 0; i < length; ++i)
        {
            int index = random.Next(0, items.Length);
            password[i] = items[index];
        }

        return new string(password);
    }

    public static string SHA512(string input)
    {
        byte[] data = Encoding.ASCII.GetBytes(input);
        SHA512 sha512 = System.Security.Cryptography.SHA512.Create();
        byte[] result = sha512.ComputeHash(data);

        return Encoding.ASCII.GetString(result);
    }
}

