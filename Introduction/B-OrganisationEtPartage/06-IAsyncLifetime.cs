namespace Introduction.B_OrganisationEtPartage;

// Classe à tester - Service de base de données
public class DatabaseService
{
    private bool _isConnected;

    public async Task ConnectAsync()
    {
        // Simuler une connexion asynchrone à une base de données
        await Task.Delay(100); // Simule une opération async
        _isConnected = true;
        Console.WriteLine("Connexion à la base de données établie");
    }

    public async Task DisconnectAsync()
    {
        // Simuler une déconnexion asynchrone
        await Task.Delay(50); // Simule une opération async
        _isConnected = false;
        Console.WriteLine("Déconnexion de la base de données");
    }

    public async Task<string> GetDataAsync()
    {
        if (!_isConnected)
            throw new InvalidOperationException("Non connecté à la base de données");

        // Simule une récupération de données
        await Task.Delay(200);
        return "Données de test";
    }
}

// Classe de test implémentant IAsyncLifetime
public class DatabaseServiceTests : IAsyncLifetime
{
    private readonly DatabaseService _databaseService;

    public DatabaseServiceTests()
    {
        _databaseService = new DatabaseService();
    }

    // Méthode de configuration qui s'exécute de manière asynchrone avant chaque test
    public async Task InitializeAsync()
    {
        // Établir une connexion à la base de données avant chaque test
        await _databaseService.ConnectAsync();
        Console.WriteLine("Test initialisé");
    }

    // Méthode de nettoyage qui s'exécute de manière asynchrone après chaque test
    public async Task DisposeAsync()
    {
        // Fermer la connexion à la base de données après chaque test
        await _databaseService.DisconnectAsync();
        Console.WriteLine("Test nettoyé");
    }

    [Fact]
    public async Task GetDataAsync_WhenConnected_ReturnsData()
    {
        // Arrange - déjà fait dans InitializeAsync

        // Act
        var result = await _databaseService.GetDataAsync();

        // Assert
        Assert.Equal("Données de test", result);
    }

    [Fact]
    public async Task GetDataAsync_CalledMultipleTimes_ReturnsConsistentResults()
    {
        // Act
        var result1 = await _databaseService.GetDataAsync();
        var result2 = await _databaseService.GetDataAsync();

        // Assert
        Assert.Equal(result1, result2);
    }
}
