using GanhoDeCapital.Handlers;
using GanhoDeCapital.Models;
using System.Text.Json;

Console.WriteLine("Enter the operations list:");
Console.WriteLine();

var input = Console.ReadLine();
Console.Clear();
var operations = JsonSerializer.Deserialize<IEnumerable<Operation>>(input);

var results = TaxCalculator.CalculateTaxes(operations);

Console.WriteLine(JsonSerializer.Serialize(results));

