namespace Exemples.TU.A_Bases;

public class InlineData
{
    [Theory]
    [InlineData(5, 3, 8)]
    [InlineData(0, 0, 0)]
    [InlineData(-5, 5, 0)]
    [InlineData(10, -3, 7)]
    [InlineData(int.MaxValue, 1, int.MinValue)] // Test de dépassement
    public void Addition_WithVariousInputs_ShouldReturnExpectedResults(int a, int b, int expected)
    {
        // Arrange
        var calculator = new Object_InlineData();

        // Act
        int result = calculator.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
}

public class Object_InlineData
{
    public int Add(int a, int b)
        => a + b;
}