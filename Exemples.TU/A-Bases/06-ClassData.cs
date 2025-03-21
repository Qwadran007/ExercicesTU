using System.Collections;

namespace Exemples.TU.A_Bases;

public class ClassData
{
    // Utilisation de ClassData pour injecter les données de test
    [Theory]
    [ClassData(typeof(MultiplicationTestData))]
    public void Multiply_ShouldReturnCorrectProduct(int a, int b, int expected)
    {
        // Arrange
        var calculator = new Object_ClassData();

        // Act
        var result = calculator.Multiply(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
}

public class MultiplicationTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 1, 2, 2 };
        yield return new object[] { -1, 5, -5 };
        yield return new object[] { 0, 10, 0 };
        yield return new object[] { 3, 4, 12 };
        yield return new object[] { -2, -3, 6 };
    }

    // Implémentation de l'interface non générique IEnumerable qui délègue à notre version générique
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class Object_ClassData
{
    public int Multiply(int a, int b)
        => a * b;
}