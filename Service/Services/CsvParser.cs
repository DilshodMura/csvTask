using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Services;
using Service.ServiceModels;
using System.Globalization;
using System.Reflection;

namespace Service.Services
{
    public sealed class CsvParser : ICsvParser
    {
        public IEnumerable<IUser> ParseCsvFile(Stream stream)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };

            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, configuration);

            // Read the header row using the following method
            if (!csv.ReadHeader())
            {
                // Handle the case where the header is not present in the CSV file
                throw new Exception("CSV file does not contain a header row.");
            }

            var headers = csv.Context.HeaderRecord;
            var userProperties = typeof(User).GetProperties(); // Use the concrete class 'User'

            var propertyMappings = new Dictionary<string, PropertyInfo>();

            // Dynamically map the headers to the IUser interface properties
            foreach (var header in headers)
            {
                var matchingProperty = userProperties.FirstOrDefault(prop => string.Equals(prop.Name, header, StringComparison.OrdinalIgnoreCase));
                if (matchingProperty != null)
                {
                    propertyMappings[header] = matchingProperty;
                }
            }

            var users = new List<User>();

            while (csv.Read())
            {
                var user = new User();

                foreach (var kvp in propertyMappings)
                {
                    var header = kvp.Key;
                    var property = kvp.Value;

                    var value = csv.GetField(header);
                    if (value != null)
                    {
                        var convertedValue = Convert.ChangeType(value, property.PropertyType, CultureInfo.InvariantCulture);
                        property.SetValue(user, convertedValue);
                    }
                }

                users.Add(user);
            }

            return users;
        }
    }
}
