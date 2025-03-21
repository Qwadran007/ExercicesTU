namespace Exemples.TU.B_OrganisationEtPartage;

// Exemple avancé avec une fixture partagée qui implémente IAsyncLifetime
public class SharedDatabaseFixture : IAsyncLifetime
{
    public DatabaseService Service { get; private set; }

    public SharedDatabaseFixture()
    {
        Service = new DatabaseService();
    }

    public async Task InitializeAsync()
    {
        // Configuration unique pour tous les tests qui utilisent cette fixture
        await Service.ConnectAsync();
        Console.WriteLine("Fixture partagée initialisée");
    }

    public async Task DisposeAsync()
    {
        // Nettoyage unique après tous les tests
        await Service.DisconnectAsync();
        Console.WriteLine("Fixture partagée nettoyée");
    }
}

// Classe de test utilisant la fixture partagée
public class SharedDatabaseTests : IClassFixture<SharedDatabaseFixture>
{
    private readonly SharedDatabaseFixture _fixture;

    public SharedDatabaseTests(SharedDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetDataAsync_UsingSharedFixture_ReturnsData()
    {
        // Act
        var result = await _fixture.Service.GetDataAsync();

        // Assert
        Assert.Equal("Données de test", result);
    }
}