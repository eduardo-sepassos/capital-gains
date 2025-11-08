using GanhoDeCapital.Handlers;
using GanhoDeCapital.Models;
using System.Text.Json;

namespace CapitalGains.Handlers;

public static class OpertionsProcessor
{
    public static string ProcessOperations(string input)
    {
        string output = string.Empty;

        foreach (var item in FormatInput(input))
        {
            var operations = JsonSerializer.Deserialize<IEnumerable<Operation>>(item);

            var results = new TaxCalculator(operations)
                            .Calculate();

           output += JsonSerializer.Serialize(results);
        }

        return output;
    }

    private static IEnumerable<string> FormatInput(string input)
    {
        return input.Split('[', StringSplitOptions.RemoveEmptyEntries).Select(item => "[" + item);
    }
}
