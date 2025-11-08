using CapitalGains.Handlers;
using FluentAssertions;

namespace Tests.Integration;
public class UserInputTests
{
    [Fact]
    public void Input_With_More_Than_One_Line_Is_Handled_Successfully()
    {
        var input = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}][{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]";

        var result = OpertionsProcessor.ProcessOperations(input);

        result.Should().Be("[{\"tax\":0},{\"tax\":0},{\"tax\":0}][{\"tax\":0},{\"tax\":10000},{\"tax\":0}]");
    }

    [Fact]
    public void InputWith_One_Line_Handled_Successfully()
    {
        var input = "[{\"operation\": \"buy\", \"unit-cost\": 5000.00, \"quantity\": 10},{\"operation\": \"sell\", \"unit-cost\": 4000.00, \"quantity\": 5},{\"operation\": \"buy\", \"unit-cost\": 15000.00, \"quantity\": 5},{\"operation\": \"buy\", \"unit-cost\": 4000.00, \"quantity\": 2},{\"operation\": \"buy\", \"unit-cost\": 23000.00, \"quantity\": 2},{\"operation\": \"sell\", \"unit-cost\": 20000.00, \"quantity\": 1},{\"operation\": \"sell\", \"unit-cost\": 12000.00, \"quantity\": 10},{\"operation\": \"sell\", \"unit-cost\": 15000.00, \"quantity\": 3}]";

        var result = OpertionsProcessor.ProcessOperations(input);

        result.Should().Be("[{\"tax\":0},{\"tax\":0},{\"tax\":0},{\"tax\":0},{\"tax\":0},{\"tax\":0},{\"tax\":1000},{\"tax\":2400}]");
    }
}
