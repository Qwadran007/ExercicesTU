namespace Introduction.A_Bases;

public class MemberData
{
    public static IEnumerable<object[]> AdditionTestData()
    {
        yield return new object[] { 1, 2, 3 };
        yield return new object[] { -1, 1, 0 };
        yield return new object[] { 0, 0, 0 };
        yield return new object[] { 10, 20, 30 };
        yield return new object[] { -5, -10, -15 };
    }

    [Theory]
    [MemberData(nameof(AdditionTestData))]
    public void Add_ShouldReturnCorrectSum(int a, int b, int expected)
    {
        // Arrange
        var calculator = new Object_MemberData();

        // Act
        var result = calculator.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
}

public class Object_MemberData
{
    public int Add(int a, int b)
        => a + b;
}