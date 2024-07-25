using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FinancialDocument.Application.Extensions;

public static class StringExtensions
{
    public static string AnonymizeJson(this string json, IReadOnlyList<string> unchangedProperties, IReadOnlyList<string> hashedProperties)
    {
        // Regex to match all JSON properties and values
        string pattern = @"""([^""]+)"":\s*(""[^""]*""|\d+(\.\d+)?|\[|\{|\}|[^\],\s]+)";

        string result = Regex.Replace(json, pattern, match =>
        {
            string propertyName = match.Groups[1].Value;
            string propertyValue = match.Groups[2].Value;

            if (unchangedProperties.Contains(propertyName))
            {
                return $"\"{propertyName}\": {propertyValue}";
            }
            else if (hashedProperties.Contains(propertyName))
            {
                string hashedValue = HashValue(propertyValue.Trim('"'));
                return $"\"{propertyName}\": \"{hashedValue}\"";
            }
            else
            {
                return $"\"{propertyName}\": \"####\"";
            }
        });

        return result;
    }

    static string HashValue(string value)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
