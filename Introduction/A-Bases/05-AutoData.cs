using AutoFixture.Xunit2;

namespace Introduction.A_Bases;


public class AutoDataTests
{
    [Theory]
    [AutoData]
    public void FullName_ReturnsFirstNameAndLastName(string firstName, string lastName)
    {
        // Arrange
        var person = new Client { FirstName = firstName, LastName = lastName };
        var expected = $"{firstName} {lastName}";

        // Act
        var result = person.GetFullName();

        // Assert
        Assert.Equal(expected, result);
    }
}

public class Client
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}