namespace GanhoDeCapital.Models;
public class Position
{
    private const decimal UNPAYABLE_MAX_VALUE = 20000;
    public long Units { get; set; } = 0;
    public decimal AveragePrice { get; set; } = 0;
    public decimal Loss { get; set; } = 0;

    public Tax HandleOperation(Operation operation)
    {
        var strategy = SetStrategy(operation);

        var tax = strategy();

        this.Units += operation.Quantity;

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

        return new Tax(0);
    }

    private Tax HandleSell(Operation operation)
    {
        if (operation.UnitCost <= this.AveragePrice)
        {
            Loss += (this.AveragePrice - operation.UnitCost) * operation.Quantity;
            return new Tax(0);
        }

        if (operation.TotalOperationValue <= UNPAYABLE_MAX_VALUE)
            return new Tax(0);

        var profit = (operation.UnitCost - this.AveragePrice) * operation.Quantity;
        if (profit > Loss)
        {
            var prcentage = 0.2m;
            var taxAmount = Math.Round(profit * prcentage);
            Loss = 0;
            return new Tax(taxAmount);
        }

        Loss -= operation.TotalOperationValue;
        return new Tax(0);

    }
}
