using AutoBogus;
using CapitalGains.Constants;
using FluentAssertions;
using GanhoDeCapital.Handlers;
using GanhoDeCapital.Models;

namespace UnitTests;

public class TaxCalculatorTests
{
    [Fact]
    public void Tax_On_Buy_Operation_Is_Always_0()
    {
        //Arrange
        var list = new List<Operation>()
        {
            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Buy)
                .RuleFor(x => x.UnitCost, 1000m)
                .RuleFor(x => x.Quantity, 20)
                .Generate(),

            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Buy)
                .RuleFor(x => x.UnitCost, 1000m)
                .RuleFor(x => x.Quantity, 20)
                .Generate(),

            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Buy)
                .RuleFor(x => x.UnitCost, 1000m)
                .RuleFor(x => x.Quantity, 20)
                .Generate()
        };

        //Act
        var taxes = new TaxCalculator(list).Calculate();

        //Assert
        taxes.All(x => x.TaxAmout == 0).Should().BeTrue();
    }

    [Fact]
    public void Sell_Operations_Under_20000_Not_Taxable()
    {
        //Arrange
        var list = new List<Operation>()
        {
            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Buy)
                .RuleFor(x => x.UnitCost, 20)
                .RuleFor(x => x.Quantity, 5000)
                .Generate(),

            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Sell)
                .RuleFor(x => x.UnitCost, 1000m)
                .RuleFor(x => x.Quantity, 20)
                .Generate(),

            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Sell)
                .RuleFor(x => x.UnitCost, 1000m)
                .RuleFor(x => x.Quantity, 19)
                .Generate()
        };

        //Act
        var taxes = new TaxCalculator(list).Calculate();

        //Assert
        taxes.All(x => x.TaxAmout == 0).Should().BeTrue();
    }

    [Fact]
    public void Sell_Operations_Greater_Than_20000_Taxable()
    {
        //Arrange
        var list = new List<Operation>()
        {
            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Buy)
                .RuleFor(x => x.UnitCost, 20)
                .RuleFor(x => x.Quantity, 5000)
                .Generate(),

            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Sell)
                .RuleFor(x => x.UnitCost, 100)
                .RuleFor(x => x.Quantity, 5000)
                .Generate(),
        };

        //Act
        var taxes = new TaxCalculator(list).Calculate();

        //Assert
        taxes.Should().ContainSingle(x => x.TaxAmout > 0);
        taxes.Should().ContainSingle(x => x.TaxAmout == 0);
    }

    [Fact]
    public void Sell_Operations_20Percent_Tax_Over_Profit()
    {
        //Arrange
        decimal buyPrice = 20;
        decimal sellPrice = 100;
        long quantity = 5000;

        var list = new List<Operation>()
        {
            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Buy)
                .RuleFor(x => x.UnitCost, buyPrice)
                .RuleFor(x => x.Quantity, quantity)
                .Generate(),

            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Sell)
                .RuleFor(x => x.UnitCost, sellPrice)
                .RuleFor(x => x.Quantity, quantity)
                .Generate(),
        };

        var profit = (sellPrice - buyPrice) * quantity;

        //Act
        var taxes = new TaxCalculator(list).Calculate();

        //Assert
        taxes.Should().ContainSingle(x => x.TaxAmout > 0);
        taxes.Should().ContainSingle(x => x.TaxAmout == profit * OperationConstants.TaxInterest);
        taxes.Should().ContainSingle(x => x.TaxAmout == 0);
    }

    [Fact]
    public void Loss_Not_Taxable()
    {
        //Arrange
        var list = new List<Operation>()
        {
            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Buy)
                .RuleFor(x => x.UnitCost, 20)
                .RuleFor(x => x.Quantity, 5000)
                .Generate(),

            new AutoFaker<Operation>()
                .RuleFor(x => x.OperationType, OperationType.Sell)
                .RuleFor(x => x.UnitCost, 15)
                .RuleFor(x => x.Quantity, 5000)
                .Generate(),
        };

        //Act
        var taxes = new TaxCalculator(list).Calculate();

        //Assert
        taxes.All(x => x.TaxAmout == 0).Should().BeTrue();
    }

    [Fact]
    public void PreviuosLossDeducedOverProfit()
    {
        //Arrange
        var buyOperation = new AutoFaker<Operation>()
            .RuleFor(x => x.OperationType, OperationType.Buy)
            .RuleFor(x => x.UnitCost, 20)
            .RuleFor(x => x.Quantity, 5000)
            .Generate();

        var lossOperation = new AutoFaker<Operation>()
            .RuleFor(x => x.OperationType, OperationType.Sell)
            .RuleFor(x => x.UnitCost, 15)
            .RuleFor(x => x.Quantity, 1000)
            .Generate();

        var profitOperation = new AutoFaker<Operation>()
            .RuleFor(x => x.OperationType, OperationType.Sell)
            .RuleFor(x => x.UnitCost, 30)
            .RuleFor(x => x.Quantity, 4000)
            .Generate();

        var list = new List<Operation>()
        {
            buyOperation,
            lossOperation,
            profitOperation
        };

        var loss = (buyOperation.UnitCost - lossOperation.UnitCost) * lossOperation.Quantity;
        var profit = (profitOperation.UnitCost - buyOperation.UnitCost) * profitOperation.Quantity;

        var expectedTax = (profit - loss) * OperationConstants.TaxInterest;

        //Act
        var taxes = new TaxCalculator(list).Calculate();

        //Assert
        taxes.Should().ContainSingle(x => x.TaxAmout == expectedTax);
    }
}
