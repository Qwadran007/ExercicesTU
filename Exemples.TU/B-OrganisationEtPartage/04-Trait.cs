namespace Exemples.TU.B_OrganisationEtPartage;

public class CalculatorTests
{
    // Test avec un trait de catégorie
    [Fact]
    [Trait("Category", "Addition")]
    public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(5, 3);

        // Assert
        Assert.Equal(8, result);
    }

    // Test avec plusieurs traits
    [Fact]
    [Trait("Category", "Multiplication")]
    [Trait("Priority", "High")]
    public void Multiply_TwoPositiveNumbers_ReturnsCorrectProduct()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Multiply(4, 5);

        // Assert
        Assert.Equal(20, result);
    }

    // Test avec trait pour environnement
    [Fact]
    [Trait("Environment", "Development")]
    [Trait("Category", "Subtraction")]
    public void Subtract_FirstGreaterThanSecond_ReturnsPositiveNumber()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Subtract(10, 4);

        // Assert
        Assert.Equal(6, result);
    }

    // Test avec trait de performance
    [Fact]
    [Trait("Type", "Performance")]
    [Trait("Category", "Division")]
    public void Divide_ValidDivision_ExecutesQuickly()
    {
        // Arrange
        var calculator = new Calculator();

        // Act & Assert
        Assert.Equal(5, calculator.Divide(10, 2));
    }

    // Test qui doit être exécuté uniquement en intégration continue
    [Fact]
    [Trait("CI", "Required")]
    [Trait("Category", "Division")]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        // Arrange
        var calculator = new Calculator();

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(5, 0));
    }
}

// Une autre classe de test avec différents traits
public class StringHelperTests
{
    [Fact]
    [Trait("Category", "String")]
    [Trait("Priority", "Low")]
    public void Reverse_ValidString_ReturnsReversedString()
    {
        // Arrange
        var helper = new StringHelper();

        // Act
        string result = helper.Reverse("hello");

        // Assert
        Assert.Equal("olleh", result);
    }

    [Fact]
    [Trait("Category", "String")]
    [Trait("OS", "Windows")]
    public void Concat_TwoStrings_ReturnsConcatenatedString()
    {
        // Arrange
        var helper = new StringHelper();

        // Act
        string result = helper.Concat("Hello, ", "World!");

        // Assert
        Assert.Equal("Hello, World!", result);
    }
}

// Classes de démonstration utilisées dans les tests
public class Calculator
{
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
    public int Multiply(int a, int b) => a * b;
    public int Divide(int a, int b) => a / b;
}

public class StringHelper
{
    public string Reverse(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public string Concat(string a, string b) => a + b;
}