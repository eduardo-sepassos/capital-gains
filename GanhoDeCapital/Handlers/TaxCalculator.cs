using GanhoDeCapital.Models;

namespace GanhoDeCapital.Handlers;
public static class TaxCalculator
{
    public static IEnumerable<Tax> CalculateTaxes(IEnumerable<Operation> operations)
    {
        var taxes = new List<Tax>();

        var position = new Position();

        foreach (var operation in operations)
        {
            var tax = position.HandleOperation(operation);

            taxes.Add(tax);
        }

        return taxes;
    }
}
