namespace Introduction.A_Bases;

public class AssertTests
{
    [Fact]
    public async Task DemonstrateVariousAssertTypes()
    {
        // Arrange
        string textValue = "Hello xUnit";
        int number = 42;
        List<string> fruits = new List<string> { "Pomme", "Banane", "Orange" };
        DateTime today = DateTime.Today;
        Person person = new Person { Id = 1, Name = "Alice", Age = 30 };

        // Act & Assert - Égalité
        Assert.Equal("Hello xUnit", textValue);
        Assert.NotEqual("hello xunit", textValue); // Sensible à la casse

        // Assertions numériques
        Assert.Equal(42, number);
        Assert.NotEqual(0, number);
        Assert.InRange(number, 1, 100);
        Assert.True(number > 40);
        Assert.False(number > 50);

        // Collections
        Assert.Contains("Banane", fruits);
        Assert.DoesNotContain("Kiwi", fruits);
        Assert.Single([5]);
        Assert.Empty(new List<string>());
        Assert.Collection(fruits,
            item => Assert.Equal("Pomme", item),
            item => Assert.Equal("Banane", item),
            item => Assert.Equal("Orange", item)
        );

        // Références et types
        Assert.NotNull(person);
        Assert.IsType<Person>(person);
        Assert.IsNotType<string>(person);

        // Propriété et membre d'objet
        var newPerson = new Person { Id = 1, Name = "Alice", Age = 30 };
        Assert.Equal(person.Name, newPerson.Name);
        Assert.Equal(person.Age, newPerson.Age);
        // Comparaison approximative (pour les nombres flottants)
        Assert.Equal(0.33, 1.0 / 3.0, 2); // Précision à 2 décimales

        // Assertions avec messages personnalisés 
        Assert.True(person.Age >= 18, "La personne doit être majeure");

        // Vérification d'exceptions
        var ex = Assert.Throws<ArgumentException>(() => ThrowExceptionMethod(""));
        Assert.Equal("Le message ne peut pas être vide", ex.Message);

        // Vérification de tâches asynchrones
        await Assert.ThrowsAsync<TimeoutException>(() => ThrowAsyncExceptionMethod());
    }

    private string ThrowExceptionMethod(string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentException("Le message ne peut pas être vide");

        return message;
    }

    private async Task ThrowAsyncExceptionMethod()
    {
        await Task.Delay(10);
        throw new TimeoutException("Opération expirée");
    }
}

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}