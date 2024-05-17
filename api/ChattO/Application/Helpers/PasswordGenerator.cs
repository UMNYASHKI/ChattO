using System.Security.Cryptography;

namespace Application.Helpers;

public class PasswordGenerator
{
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Digits = "0123456789";
    private const string SpecialCharacters = "!@&*()=<>?";
    private static int DefaultLength = 8;

    public static string GenerateRandomPassword()
    {
        var characterSet = Uppercase + Lowercase + Digits + SpecialCharacters;
        var password = new char[DefaultLength];

        password[0] = Uppercase[GetRandomInt(Uppercase.Length)];
        password[1] = Lowercase[GetRandomInt(Lowercase.Length)];
        password[2] = Digits[GetRandomInt(Digits.Length)];
        password[3] = SpecialCharacters[GetRandomInt(SpecialCharacters.Length)];

        for (int i = 4; i < DefaultLength; i++)
        {
            password[i] = characterSet[GetRandomInt(characterSet.Length)];
        }

        return new string(password.OrderBy(x => GetRandomInt(DefaultLength)).ToArray());
    }

    private static int GetRandomInt(int max)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var data = new byte[4];
            rng.GetBytes(data);
            return Math.Abs(BitConverter.ToInt32(data, 0)) % max;
        }
    }
}
