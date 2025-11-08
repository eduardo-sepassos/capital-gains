using CapitalGains.Constants;
using GanhoDeCapital.Models;

namespace GanhoDeCapital.Handlers;
public class TaxCalculator
{
    private readonly List<Operation> _operations = new();
    private List<Tax> _taxes;
    private long Units { get; set; }
    private decimal AveragePrice { get; set; }
    private decimal Loss { get; set; }
    private bool NotProfitable(decimal operationUnitCost) => operationUnitCost <= AveragePrice;
    private bool BellowOrEqualNonTaxableValue(decimal totalOperationValue) => totalOperationValue <= OperationConstants.NonTaxableMaxValue;
    private bool ProfitGreatherThanLoss(decimal profit) => profit > Loss;

    public TaxCalculator(IEnumerable<Operation> operations)
    {
        _operations = operations.ToList();
        _taxes = new();
    }

    public IEnumerable<Tax> Calculate()
    {
        foreach (var operation in _operations)
        {
            var tax = Handle(operation);

            _taxes.Add(tax);
        }

        return _taxes;
    }

    private Tax Handle(Operation operation)
    {
        switch (operation.OperationType)
        {
            case OperationType.Buy:
                return HandleBuy(operation);

            case OperationType.Sell:
                return HandleSell(operation);

            default: throw new NotImplementedException($"OperationType of {operation.OperationType} not supported");
        }
    }

    private Tax HandleBuy(Operation operation)
    {
        var currentValue = Units * AveragePrice;

        var newAverage = (currentValue + operation.TotalOperationValue) / (Units + operation.Quantity);

        AveragePrice = Math.Round(newAverage, 2);
        Units += operation.Quantity;

        return new Tax(0);
    }

    private Tax HandleSell(Operation operation)
    {
        if (NotProfitable(operation.UnitCost))
        {
            Loss += (AveragePrice - operation.UnitCost) * operation.Quantity;
            Units -= operation.Quantity;
            return new Tax(0);
        }

        if (BellowOrEqualNonTaxableValue(operation.TotalOperationValue))
        {
            Units -= operation.Quantity;
            return new Tax(0);
        }

        var profit = (operation.UnitCost - AveragePrice) * operation.Quantity;

        if (ProfitGreatherThanLoss(profit))
        {
            var amountAfterDeduction = profit - Loss;
            var taxAmount = Math.Round(amountAfterDeduction * OperationConstants.TaxInterest);
            Loss = 0;
            Units -= operation.Quantity;
            return new Tax(taxAmount);
        }

        Loss -= profit;
        Units -= operation.Quantity;
        return new Tax(0);

    }
}
