using FluentAssertions;
using GanhoDeCapital.Handlers;
using GanhoDeCapital.Models;
namespace Tests.UnitTests.Cases;
public class CasesTests
{
    [Fact]
    public void Case1_Outputs_As_Expected()
    {
        //Arrange
        var operations = new List<Operation>()
        {
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 10,
                Quantity = 100
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 15,
                Quantity = 50
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 15,
                Quantity = 50
            },
        };

        //Act
        var taxes = new TaxCalculator(operations).Calculate();

        //Assert
        taxes.All(x => x.TaxAmout == 0);
    }

    [Fact]
    public void Case2_Outputs_As_Expected()
    {
        //Arrange
        var operations = new List<Operation>()
        {
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 10,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 20,
                Quantity = 5000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 5,
                Quantity = 5000
            },
        };

        //Act
        var taxes = new TaxCalculator(operations).Calculate();

        //Assert
        taxes.ElementAt(0).TaxAmout.Should().Be(0);
        taxes.ElementAt(1).TaxAmout.Should().Be(10000);
        taxes.ElementAt(2).TaxAmout.Should().Be(0);
    }

    [Fact]
    public void Case3_Outputs_As_Expected()
    {
        //Arrange
        var operations = new List<Operation>()
        {
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 10,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 5,
                Quantity = 5000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 20,
                Quantity = 3000
            },
        };

        //Act
        var taxes = new TaxCalculator(operations).Calculate();

        //Assert
        taxes.ElementAt(0).TaxAmout.Should().Be(0);
        taxes.ElementAt(1).TaxAmout.Should().Be(0);
        taxes.ElementAt(2).TaxAmout.Should().Be(1000);
    }

    [Fact]
    public void Case4_Outputs_As_Expected()
    {
        //Arrange
        var operations = new List<Operation>()
        {
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 10,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 25,
                Quantity = 5000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 15,
                Quantity = 10000
            },
        };

        //Act
        var taxes = new TaxCalculator(operations).Calculate();

        //Assert
        taxes.ElementAt(0).TaxAmout.Should().Be(0);
        taxes.ElementAt(1).TaxAmout.Should().Be(0);
        taxes.ElementAt(2).TaxAmout.Should().Be(0);
    }

    [Fact]
    public void Case5_Outputs_As_Expected()
    {
        //Arrange
        var operations = new List<Operation>()
        {
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 10,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 25,
                Quantity = 5000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 15,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 25,
                Quantity = 5000
            },
        };

        //Act
        var taxes = new TaxCalculator(operations).Calculate();

        //Assert
        taxes.ElementAt(0).TaxAmout.Should().Be(0);
        taxes.ElementAt(1).TaxAmout.Should().Be(0);
        taxes.ElementAt(2).TaxAmout.Should().Be(0);
        taxes.ElementAt(3).TaxAmout.Should().Be(10000);
    }

    [Fact]
    public void Case6_Outputs_As_Expected()
    {
        //Arrange
        var operations = new List<Operation>()
        {
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 10,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 2,
                Quantity = 5000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 20,
                Quantity = 2000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 20,
                Quantity = 2000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 25,
                Quantity = 1000
            },
        };

        //Act
        var taxes = new TaxCalculator(operations).Calculate();

        //Assert
        taxes.ElementAt(0).TaxAmout.Should().Be(0);
        taxes.ElementAt(1).TaxAmout.Should().Be(0);
        taxes.ElementAt(2).TaxAmout.Should().Be(0);
        taxes.ElementAt(3).TaxAmout.Should().Be(0);
        taxes.ElementAt(4).TaxAmout.Should().Be(3000);
    }

    [Fact]
    public void Case7_Outputs_As_Expected()
    {
        //Arrange
        var operations = new List<Operation>()
        {
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 10,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 2,
                Quantity = 5000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 20,
                Quantity = 2000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 20,
                Quantity = 2000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 25,
                Quantity = 1000
            },
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 20,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 15,
                Quantity = 5000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 30,
                Quantity = 4350
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 30,
                Quantity = 350
            },

        };

        //Act
        var taxes = new TaxCalculator(operations).Calculate();

        //Assert
        taxes.ElementAt(0).TaxAmout.Should().Be(0);
        taxes.ElementAt(1).TaxAmout.Should().Be(0);
        taxes.ElementAt(2).TaxAmout.Should().Be(0);
        taxes.ElementAt(3).TaxAmout.Should().Be(0);
        taxes.ElementAt(4).TaxAmout.Should().Be(3000);
        taxes.ElementAt(5).TaxAmout.Should().Be(0);
        taxes.ElementAt(6).TaxAmout.Should().Be(0);
        taxes.ElementAt(7).TaxAmout.Should().Be(3700);
        taxes.ElementAt(8).TaxAmout.Should().Be(0);

    }

    [Fact]
    public void Case8_Outputs_As_Expected()
    {
        //Arrange
        var operations = new List<Operation>()
        {
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 10,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 50,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 20,
                Quantity = 10000
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 50,
                Quantity = 10000
            }
        };

        //Act
        var taxes = new TaxCalculator(operations).Calculate();

        //Assert
        taxes.ElementAt(0).TaxAmout.Should().Be(0);
        taxes.ElementAt(1).TaxAmout.Should().Be(80000);
        taxes.ElementAt(2).TaxAmout.Should().Be(0);
        taxes.ElementAt(3).TaxAmout.Should().Be(60000);
    }

    [Fact]
    public void Case9_Outputs_As_Expected()
    {
        //Arrange
        var operations = new List<Operation>()
        {
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 5000,
                Quantity = 10
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 4000,
                Quantity = 5
            },
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 15000,
                Quantity = 5
            },
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 4000,
                Quantity = 2
            },
            new Operation
            {
                OperationType = OperationType.Buy,
                UnitCost = 23000,
                Quantity = 2
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 20000,
                Quantity = 1
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 12000,
                Quantity = 10
            },
            new Operation
            {
                OperationType = OperationType.Sell,
                UnitCost = 15000,
                Quantity = 3
            },
        };

        //Act
        var taxes = new TaxCalculator(operations).Calculate();

        //Assert
        taxes.ElementAt(0).TaxAmout.Should().Be(0);
        taxes.ElementAt(1).TaxAmout.Should().Be(0);
        taxes.ElementAt(2).TaxAmout.Should().Be(0);
        taxes.ElementAt(3).TaxAmout.Should().Be(0);
        taxes.ElementAt(4).TaxAmout.Should().Be(0);
        taxes.ElementAt(5).TaxAmout.Should().Be(0);
        taxes.ElementAt(6).TaxAmout.Should().Be(1000);
        taxes.ElementAt(7).TaxAmout.Should().Be(2400);
    }
}
