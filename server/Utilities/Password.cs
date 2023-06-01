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
}

