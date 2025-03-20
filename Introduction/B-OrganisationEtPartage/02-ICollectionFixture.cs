namespace Introduction.B_OrganisationEtPartage;

public class SharedResourceFixture : IDisposable
{
    private readonly List<string> _testData;

    public SharedResourceFixture()
    {
        // Code d'initialisation qui s'exécute une seule fois avant tous les tests
        Console.WriteLine("SharedResourceFixture: Initialisation des ressources partagées");

        // Initialisation coûteuse simulée
        Thread.Sleep(1000);

        // Création des données de test
        _testData = new List<string> { "Donnée 1", "Donnée 2", "Donnée 3" };

        Counter = 0;
    }

    public void Dispose()
    {
        // Nettoyage qui s'exécute une seule fois après tous les tests
        Console.WriteLine("SharedResourceFixture: Libération des ressources partagées");
        _testData.Clear();
    }

    // Propriétés et méthodes accessibles aux tests
    public IReadOnlyList<string> TestData => _testData;

    // Compteur partagé pour démontrer l'état partagé
    public int Counter { get; set; }

    public void IncrementCounter()
    {
        Counter++;
    }
}

// 2. Définir l'interface de collection
[CollectionDefinition("Shared Resource Collection")]
public class SharedResourceCollection : ICollectionFixture<SharedResourceFixture>
{
    // Cette classe ne contient aucun code, elle sert juste à déclarer la collection
}

// 3. Créer les classes de test qui utilisent la collection
[Collection("Shared Resource Collection")]
public class FirstTestClass
{
    private readonly SharedResourceFixture _fixture;

    public FirstTestClass(SharedResourceFixture fixture)
    {
        _fixture = fixture;
        Console.WriteLine($"FirstTestClass: Constructeur, compteur = {_fixture.Counter}");
    }

    [Fact]
    public void Test1_IncrementsCounter()
    {
        // Arrange - initial state from the fixture
        int initialValue = _fixture.Counter;

        // Act
        _fixture.IncrementCounter();

        // Assert
        Assert.Equal(initialValue + 1, _fixture.Counter);
        Console.WriteLine($"Test1_IncrementsCounter: Compteur = {_fixture.Counter}");
    }

    [Fact]
    public void Test2_AccessesSharedData()
    {
        // Act & Assert
        Assert.Equal(3, _fixture.TestData.Count);
        Assert.Contains("Donnée 2", _fixture.TestData);
        Console.WriteLine($"Test2_AccessesSharedData: Compteur = {_fixture.Counter}");
    }
}

// Une deuxième classe de test dans la même collection
[Collection("Shared Resource Collection")]
public class SecondTestClass
{
    private readonly SharedResourceFixture _fixture;

    public SecondTestClass(SharedResourceFixture fixture)
    {
        _fixture = fixture;
        Console.WriteLine($"SecondTestClass: Constructeur, compteur = {_fixture.Counter}");
    }

    [Fact]
    public void Test3_IncrementsCounter()
    {
        // Arrange - initial state from the fixture
        int initialValue = _fixture.Counter;

        // Act
        _fixture.IncrementCounter();

        // Assert
        Assert.Equal(initialValue + 1, _fixture.Counter);
        Console.WriteLine($"Test3_IncrementsCounter: Compteur = {_fixture.Counter}");
    }

    [Fact]
    public void Test4_AccessesSharedData()
    {
        // Act & Assert
        Assert.NotEmpty(_fixture.TestData);
        Assert.Equal("Donnée 1", _fixture.TestData[0]);
        Console.WriteLine($"Test4_AccessesSharedData: Compteur = {_fixture.Counter}");
    }
}