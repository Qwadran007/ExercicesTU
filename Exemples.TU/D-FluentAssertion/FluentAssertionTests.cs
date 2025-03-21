using FluentAssertions;
using Exemples.TU.E_TestDataBuilder;

namespace Exemples.TU.D_FluentAssertion;

public class FluentAssertionsExamples
{
    // 1. Assertions sur les chaînes de caractères
    [Fact]
    public void StringAssertions()
    {
        string text = "Bonjour le monde";

        text.Should().StartWith("Bonjour");
        text.Should().EndWith("monde");
        text.Should().Contain("le");
        text.Should().NotContain("au revoir");
        text.Should().HaveLength(16);
        text.Should().NotBeUpperCased();
        text.ToUpper().Should().BeUpperCased();
    }

    // 2. Assertions sur les nombres
    [Fact]
    public void NumericAssertions()
    {
        int value = 5;
        double pi = 3.14159;

        value.Should().Be(5);
        value.Should().BePositive();
        value.Should().BeGreaterThan(3);
        value.Should().BeLessThan(10);
        value.Should().BeInRange(1, 10);

        pi.Should().BeApproximately(3.14, 0.01);
    }

    // 3. Assertions sur les booléens
    [Fact]
    public void BooleanAssertions()
    {
        bool isTrue = true;
        bool isFalse = false;

        isTrue.Should().BeTrue();
        isFalse.Should().BeFalse();
    }

    // 4. Assertions sur les collections
    [Fact]
    public void CollectionAssertions()
    {
        var numbers = new[] { 1, 2, 3, 4, 5 };
        var emptyList = new List<string>();

        numbers.Should().HaveCount(5);
        numbers.Should().Contain(3);
        numbers.Should().ContainInOrder(new[] { 1, 2, 3 });
        numbers.Should().BeInAscendingOrder();
        numbers.Should().OnlyContain(n => n > 0 && n <= 5);

        emptyList.Should().BeEmpty(" est vide");
        numbers.Should().NotBeEmpty();

        // Comparaison de collections
        var otherNumbers = new[] { 1, 2, 3, 4, 5 };
        numbers.Should().BeEquivalentTo(otherNumbers);
    }

    // 5. Assertions sur les dictionnaires
    [Fact]
    public void DictionaryAssertions()
    {
        var dict = new Dictionary<string, int>
        {
            { "un", 1 },
            { "deux", 2 },
            { "trois", 3 }
        };

        dict.Should().ContainKey("un");
        dict.Should().ContainValue(3);
    }

    // 6. Assertions sur les objets et les références
    [Fact]
    public void ObjectAssertions()
    {
        var person1 = new Person { Id = 1, Name = "Alice" };
        var person2 = new Person { Id = 1, Name = "Alice" };
        object nullObj = null;

        person1.Should().NotBeNull();
        nullObj.Should().BeNull();

        // Vérification de propriétés individuelles
        person1.Should().Match<Person>(p => p.Id == 1 && p.Name == "Alice");

        // Équivalence structurelle (compare les valeurs des propriétés)
        person1.Should().BeEquivalentTo(person2);

        // Si on veut vérifier la même référence
        person1.Should().NotBeSameAs(person2);
    }

    // 7. Assertions sur les exceptions
    [Fact]
    public void ExceptionAssertions()
    {
        // Exception synchrone
        Action action = () => throw new ArgumentException("Paramètre invalide");

        action.Should().Throw<ArgumentException>()
              .WithMessage("*invalide*");

        // Vérifier qu'aucune exception n'est levée
        Action safeAction = () => { var x = 1 + 1; };
        safeAction.Should().NotThrow();
    }

    // 8. Assertions sur les tâches asynchrones
    [Fact]
    public async Task AsyncAssertions()
    {
        // Exception asynchrone
        Func<Task> asyncAction = async () =>
        {
            await Task.Delay(10);
            throw new InvalidOperationException();
        };

        await asyncAction.Should().ThrowAsync<InvalidOperationException>();

        // Complétion d'une tâche
        Func<Task> completingTask = async () => { await Task.Delay(10); };
        await completingTask.Should().NotThrowAsync();
    }

    // 9. Assertions d'événements
    [Fact]
    public void EventAssertions()
    {
        var publisher = new EventPublisher();

        // surveille l'événement
        using var monitoredPublisher = publisher.Monitor();

        // Déclencher l'événement
        publisher.RaiseMyEvent();

        monitoredPublisher.Should().Raise("MyEvent");
    }

    // 10. Assertions temporelles (avec timeout)
    [Fact]
    public void TemporalAssertions()
    {
        var counter = 0;

        Action act = () => counter++;

        act.ExecutionTime().Should().BeLessThan(TimeSpan.FromMilliseconds(100));
    }

    // 11. Assertions personnalisées avec des messages d'erreur
    [Fact(Skip = "enlever le skip pour montrer l'erreur générée par fluent validation")]
    public void CustomErrorMessages()
    {
        var value = 5;

        value.Should().Be(10, "parce que {0} est la valeur attendue pour {1}", 10, "ce test");
    }

    // 12. Assertions avec des types génériques
    [Fact]
    public void GenericAssertions()
    {
        var value = "test";

        value.Should().BeOfType<string>();
        value.Should().BeAssignableTo<object>();
    }

    // 13. Assertions sur les fichiers et répertoires (version corrigée)
    [Fact]
    public void FileAssertions()
    {
        var path = "C:\\temp\\test.txt";

        // Exécuter seulement si le fichier existe
        if (File.Exists(path))
        {
            // Utiliser la syntaxe correcte pour les assertions de fichiers
            File.Exists(path).Should().BeTrue("le fichier devrait exister");

            // Vérifier l'extension
            Path.GetExtension(path).Should().Be(".txt", "l'extension devrait être .txt");

            // Alternativement, on peut utiliser le System.IO.FileInfo
            var fileInfo = new FileInfo(path);
            fileInfo.Exists.Should().BeTrue();
            fileInfo.Extension.Should().Be(".txt");
        }
    }

    // 14. Assertions sur les dates
    [Fact]
    public void DateTimeAssertions()
    {
        var date = new DateTime(2023, 5, 15);

        date.Should().BeAfter(new DateTime(2023, 1, 1));
        date.Should().BeBefore(new DateTime(2023, 12, 31));
        date.Should().BeOnOrAfter(new DateTime(2023, 5, 15));
        date.Should().HaveYear(2023);
        date.Should().HaveMonth(5);
        date.Should().HaveDay(15);
    }

    // 15. Assertions avec chaînage (and)
    [Fact]
    public void ChainedAssertions()
    {
        var text = "Hello World";

        text.Should().Match(x => x.Contains("") || x.Contains(""));

        text.Should()
            .StartWith("Hello")
            .And.EndWith("World")
            .And.HaveLength(11)
            .And.Contain("lo Wo");
    }
}

// Classes d'exemples
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class EventPublisher
{
    public event EventHandler MyEvent;

    public void RaiseMyEvent()
    {
        MyEvent?.Invoke(this, EventArgs.Empty);
    }
}