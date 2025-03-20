using FormationTestUnitaires.Entities;
using FormationTestUnitaires.Exceptions;
using FormationTestUnitaires.Repositories;
using FormationTestUnitaires.Services.ClientServices;
using Moq;

namespace FormationTestUnitaires.Tests.Services;

public class ClientServiceTests
{
    public async Task ObtenirClientParId_ClientExiste_RetourneClient()
    {
        // Arrange
        int clientId = 1;
        var clientAttendu = new Client
        {
            Id = clientId,
            Nom = "Client Test",
            Email = "client@test.com",
            DateInscription = DateTime.Now.AddDays(-10)
        };

        // Act

        // Assert
    }

    public async Task ObtenirTousClients_RetourneTousLesClients()
    {
        // Arrange
        var listeClients = new List<Client>
            {
                new Client { Id = 1, Nom = "Client 1", Email = "client1@test.com" },
                new Client { Id = 2, Nom = "Client 2", Email = "client2@test.com" },
                new Client { Id = 3, Nom = "Client 3", Email = "client3@test.com" }
            };

        // Act

        // Assert

    }

    public async Task CreerClient_DonneesValides_RetourneClientCree()
    {
        // Arrange
        var nouveauClient = new Client
        {
            Nom = "Nouveau Client",
            Email = "nouveau@test.com",
            Telephone = "0123456789"
        };

        // Act

        // Assert
    }

    [Theory]
    [InlineData(null, "email@test.com", "Le nom du client est obligatoire")]
    [InlineData("", "email@test.com", "Le nom du client est obligatoire")]
    [InlineData("  ", "email@test.com", "Le nom du client est obligatoire")]
    [InlineData("Nom", null, "L'email du client est obligatoire")]
    [InlineData("Nom", "", "L'email du client est obligatoire")]
    [InlineData("Nom", "  ", "L'email du client est obligatoire")]
    public async Task CreerClient_DonneesInvalides_LanceException(string nom, string email, string messageAttendu)
    {
        // Arrange
        var clientInvalide = new Client
        {
            Nom = nom,
            Email = email
        };

        // Act

        // Assert
    }

    [Fact]
    public async Task MettreAJourClient_ClientNExistePas_LanceException()
    {
        // Arrange
        var clientInexistant = new Client
        {
            Id = 999,
            Nom = "Client Inexistant",
            Email = "inexistant@test.com"
        };

        // Act

        // Assert
    }

    [Fact]
    public async Task SupprimerClient_ClientExiste_SupprimeClient()
    {
        // Arrange
        int clientId = 1;

        // Act

        // Assert
    }

    public async Task SupprimerClient_ClientNExistePas_LanceException()
    {
        // Arrange
        int clientId = 999;

        // Act

        // Assert
    }

    public async Task PasserClientPremium_ClientExiste_PasseClientPremium()
    {
        // Arrange
        int clientId = 1;
        var client = new Client
        {
            Id = clientId,
            Nom = "Client Test",
            Email = "client@test.com",
            EstPremium = false
        };

        // Act

        // Assert
    }

    public async Task RechercherClients_TermeValide_RetourneClientsCorrespondants()
    {
        // Arrange
        string terme = "test";
        var clientsAttendus = new List<Client>
            {
                new Client { Id = 1, Nom = "Client Test", Email = "client@test.com" },
                new Client { Id = 2, Nom = "Autre Test", Email = "autre@example.com" }
            };

        // Act

        // Assert

    }

    public async Task RechercherClients_TermeVide_RetourneTousLesClients(string terme)
    {
        // Arrange
        var tousLesClients = new List<Client>
            {
                new Client { Id = 1, Nom = "Client 1", Email = "client1@test.com" },
                new Client { Id = 2, Nom = "Client 2", Email = "client2@test.com" },
                new Client { Id = 3, Nom = "Client 3", Email = "client3@test.com" }
            };

        // Act


        // Assert

    }
}