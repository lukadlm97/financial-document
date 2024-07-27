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
            else if (IsListValue(propertyValue))
            {
                // Process list items
                string processedList = ProcessList(propertyValue, unchangedProperties, hashedProperties);
                return $"\"{propertyName}\": {processedList}";
            }
            else if (IsObjectValue(propertyValue))
            {
                // Process nested object
                string processedObject = propertyValue.AnonymizeJson(productProperties);
                return $"\"{propertyName}\": {processedObject}";
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

    private static string ProcessList(string listValue, HashSet<string> unchangedProperties, HashSet<string> hashedProperties)
    {
        // Remove the brackets
        string innerList = listValue.Substring(1, listValue.Length - 2).Trim();

        // Regex to match all list items (objects or values)
        string itemPattern = @"(\{.*?\})|([^,\s]+)";

        string result = Regex.Replace(innerList, itemPattern, match =>
        {
            string itemValue = match.Value;

            if (IsObjectValue(itemValue))
            {
                // Process each object in the list
                string processedObject = itemValue.AnonymizeJson(unchangedProperties, hashedProperties);
                return processedObject;
            }
            else
            {
                return itemValue; // Return the item as it is for non-object items in the list
            }
        });

        return $"[{result}]";
    }

    private static string AnonymizeJson(this string json, HashSet<string> unchangedProperties, HashSet<string> hashedProperties)
    {
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
            else if (IsListValue(propertyValue))
            {
                // Process list items
                string processedList = ProcessList(propertyValue, unchangedProperties, hashedProperties);
                return $"\"{propertyName}\": {processedList}";
            }
            else if (IsObjectValue(propertyValue))
            {
                // Process nested object
                string processedObject = propertyValue.AnonymizeJson(unchangedProperties, hashedProperties);
                return $"\"{propertyName}\": {processedObject}";
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