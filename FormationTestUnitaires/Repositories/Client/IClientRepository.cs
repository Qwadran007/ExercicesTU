using FormationTestUnitaires.Entities;

namespace FormationTestUnitaires.Repositories;

/// <summary>
/// Interface définissant les opérations du repository pour les clients
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Obtient un client par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du client</param>
    /// <returns>Le client trouvé ou null si aucun</returns>
    Task<Client> GetByIdAsync(int id);

    /// <summary>
    /// Obtient tous les clients
    /// </summary>
    /// <returns>Collection de tous les clients</returns>
    Task<IEnumerable<Client>> GetAllAsync();

    /// <summary>
    /// Ajoute un nouveau client
    /// </summary>
    /// <param name="client">Client à ajouter</param>
    /// <returns>Client ajouté avec son identifiant généré</returns>
    Task<Client> AddAsync(Client client);

    /// <summary>
    /// Met à jour un client existant
    /// </summary>
    /// <param name="client">Client avec les nouvelles valeurs</param>
    Task UpdateAsync(Client client);

    /// <summary>
    /// Supprime un client par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du client à supprimer</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Vérifie si un client existe via son identifiant
    /// </summary>
    /// <param name="id">Identifiant du client</param>
    /// <returns>True si le client existe, sinon False</returns>
    Task<bool> ExisteAsync(int id);

    /// <summary>
    /// Recherche des clients par terme de recherche (nom ou email)
    /// </summary>
    /// <param name="terme">Terme à rechercher</param>
    /// <returns>Collection des clients correspondant au terme</returns>
    Task<IEnumerable<Client>> RechercherAsync(string terme);
}
