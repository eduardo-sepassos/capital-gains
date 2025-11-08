using CapitalGains.Constants;

namespace GanhoDeCapital.Models;
public class Position
{
    public long Units { get; set; } = 0;
    public decimal AveragePrice { get; set; } = 0;
    public decimal Loss { get; set; } = 0;

    public Tax HandleOperation(Operation operation)
    {
        var strategy = SetStrategy(operation);

        var tax = strategy();

        return tax;

    }

    private Func<Tax> SetStrategy(Operation operation)
    {
        switch (operation.OperationType)
        {
            case OperationType.Buy:
                return () => HandleBuy(operation);

            case OperationType.Sell:
                return () => HandleSell(operation);

            default: throw new NotImplementedException($"OperationType of {operation.OperationType} not expected");
        }
    }

    private Tax HandleBuy(Operation operation)
    {
        var currentAverage = this.Units * this.AveragePrice;
        var operationAverage = operation.Quantity * operation.UnitCost;

        var newAverage = (currentAverage + operationAverage) / (this.Units + operation.Quantity);

        this.AveragePrice = Math.Round(newAverage, 2);
        this.Units += operation.Quantity;

        return new Tax(0);
    }

    private Tax HandleSell(Operation operation)
    {
        if (operation.UnitCost <= this.AveragePrice)
        {
            Loss += (this.AveragePrice - operation.UnitCost) * operation.Quantity;
            this.Units -= operation.Quantity;
            return new Tax(0);
        }

        if (operation.TotalOperationValue <= OperationConstants.NonTaxableMaxValue)
        {
            this.Units -= operation.Quantity;
            return new Tax(0);
        }

        var profit = (operation.UnitCost - this.AveragePrice) * operation.Quantity;
        if (profit > Loss)
        {
            var amountAfterDeduction = profit - Loss;
            var taxAmount = Math.Round(amountAfterDeduction * OperationConstants.TaxInterest);
            Loss = 0;
            this.Units -= operation.Quantity;
            return new Tax(taxAmount);
        }

        Loss -= operation.TotalOperationValue;
        this.Units -= operation.Quantity;
        return new Tax(0);

    }
}
