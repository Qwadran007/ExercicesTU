using FormationTestUnitaires.Entities;
using FormationTestUnitaires.Exceptions;
using FormationTestUnitaires.Repositories;

namespace FormationTestUnitaires.Services.ClientServices;

/// <summary>
/// Service gérant les opérations liées aux clients
/// </summary>
public class ClientService
{
    private readonly IClientRepository _clientRepository;

    /// <summary>
    /// Constructeur du service client
    /// </summary>
    /// <param name="clientRepository">Repository d'accès aux données clients</param>
    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    }

    /// <summary>
    /// Obtient un client par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du client</param>
    /// <returns>Le client trouvé</returns>
    /// <exception cref="ClientInexistantException">Si le client n'existe pas</exception>
    public async Task<Client> ObtenirClientParIdAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);

        if (client == null)
            throw new ClientInexistantException(id);

        return client;
    }

    /// <summary>
    /// Obtient tous les clients
    /// </summary>
    /// <returns>Liste de tous les clients</returns>
    public async Task<IEnumerable<Client>> ObtenirTousClientsAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    /// <summary>
    /// Crée un nouveau client
    /// </summary>
    /// <param name="client">Le client à créer</param>
    /// <returns>Le client créé avec son Id généré</returns>
    /// <exception cref="ArgumentException">Si les données du client sont invalides</exception>
    public async Task<Client> CreerClientAsync(Client client)
    {
        // Validation des données du client
        if (client == null)
            throw new ArgumentNullException(nameof(client));

        if (string.IsNullOrWhiteSpace(client.Nom))
            throw new ArgumentException("Le nom du client est obligatoire");

        if (string.IsNullOrWhiteSpace(client.Email))
            throw new ArgumentException("L'email du client est obligatoire");

        // Initialiser la date d'inscription si elle n'est pas définie
        if (client.DateInscription == default)
            client.DateInscription = DateTime.Now;

        return await _clientRepository.AddAsync(client);
    }

    /// <summary>
    /// Met à jour les informations d'un client existant
    /// </summary>
    /// <param name="client">Le client avec les informations mises à jour</param>
    /// <returns>Tâche asynchrone</returns>
    /// <exception cref="ClientInexistantException">Si le client n'existe pas</exception>
    /// <exception cref="ArgumentException">Si les données du client sont invalides</exception>
    public async Task MettreAJourClientAsync(Client client)
    {
        if (client == null)
            throw new ArgumentNullException(nameof(client));

        if (string.IsNullOrWhiteSpace(client.Nom))
            throw new ArgumentException("Le nom du client est obligatoire");

        if (string.IsNullOrWhiteSpace(client.Email))
            throw new ArgumentException("L'email du client est obligatoire");

        var existant = await _clientRepository.ExisteAsync(client.Id);
        if (!existant)
            throw new ClientInexistantException(client.Id);

        await _clientRepository.UpdateAsync(client);
    }

    /// <summary>
    /// Supprime un client par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du client à supprimer</param>
    /// <returns>Tâche asynchrone</returns>
    /// <exception cref="ClientInexistantException">Si le client n'existe pas</exception>
    public async Task SupprimerClientAsync(int id)
    {
        var existant = await _clientRepository.ExisteAsync(id);
        if (!existant)
            throw new ClientInexistantException(id);

        await _clientRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Passe un client au statut premium
    /// </summary>
    /// <param name="id">Identifiant du client</param>
    /// <returns>Tâche asynchrone</returns>
    /// <exception cref="ClientInexistantException">Si le client n'existe pas</exception>
    public async Task PasserClientPremiumAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null)
            throw new ClientInexistantException(id);

        client.EstPremium = true;
        await _clientRepository.UpdateAsync(client);
    }

    /// <summary>
    /// Recherche des clients par terme de recherche
    /// </summary>
    /// <param name="terme">Terme de recherche (nom ou email)</param>
    /// <returns>Liste des clients correspondant au terme de recherche</returns>
    public async Task<IEnumerable<Client>> RechercherClientsAsync(string terme)
    {
        if (string.IsNullOrWhiteSpace(terme))
            return await _clientRepository.GetAllAsync();

        return await _clientRepository.RechercherAsync(terme);
    }
}