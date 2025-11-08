using GanhoDeCapital.Handlers;
using GanhoDeCapital.Models;
using System.Text.Json;

Console.WriteLine("Enter the operations list:");
Console.WriteLine();

var input = Console.ReadLine();
Console.Clear();

var lines = input.Split('[', StringSplitOptions.RemoveEmptyEntries);

foreach (var item in lines)
{
    var operations = JsonSerializer.Deserialize<IEnumerable<Operation>>($"[{item}");

    var results = new TaxCalculator(operations)
                    .Calculate();

    Console.WriteLine(JsonSerializer.Serialize(results));
    
}

