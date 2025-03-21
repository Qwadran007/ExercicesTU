using Xunit.Abstractions;

namespace Exemples.TU.A_Bases;

public class FactTests
{
    private readonly ITestOutputHelper _output;

    public FactTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Addition_ShouldReturnCorrectSum()
    {
        // Arrange
        var calculator = new Object_Fact();
        int a = 5;
        int b = 3;
        int expected = 8;

        // Act
        int result = calculator.Add(a, b);

        // Assert
        _output.WriteLine("Addition_ShouldReturnCorrectSum terminé");
        Assert.Equal(expected, result);
    }
}
public class Object_Fact
{
    public int Add(int a, int b)
        => a + b;
}