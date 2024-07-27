using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using FinancialDocument.Application.Contracts.DTOs;

namespace FinancialDocument.Application.Extensions;

public static class StringExtensions
{
    public static string AnonymizeJson(this string json, List<PropertySettings> productProperties)
    {
        var unchangedProperties = productProperties
                                    .Where(x => x.Type == Domain.Enums.PropertyRepresentationType.Unchanged)
                                    .Select(x => x.Name)
                                    .ToHashSet();
        var hashedProperties = productProperties
                                .Where(x => x.Type == Domain.Enums.PropertyRepresentationType.Hash)
                                .Select(x => x.Name)
                                .ToHashSet();

        // Regex to match all JSON properties and values
        string pattern = @"""([^""]+)"":\s*((""[^""]*""|\d+(\.\d+)?|\[.*?\]|\{.*?\}|[^\],\s]+))";

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
            else if (IsListValue(propertyValue) || IsObjectValue(propertyValue))
            {
                return $"\"{propertyName}\": {propertyValue}";
            }
            else
            {
                return $"\"{propertyName}\": \"####\"";
            }
        });

        return result;
    }

    private static bool IsListValue(string value)
    {
        return value.Trim().StartsWith("[") && value.Trim().EndsWith("]");
    }

    private static bool IsObjectValue(string value)
    {
        return value.Trim().StartsWith("{") && value.Trim().EndsWith("}");
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

