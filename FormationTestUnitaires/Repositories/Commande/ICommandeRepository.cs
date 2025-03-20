using FormationTestUnitaires.Entities;

namespace FormationTestUnitaires.Repositories;

/// <summary>
/// Interface définissant les opérations du repository pour les commandes
/// </summary>
public interface ICommandeRepository
{
    /// <summary>
    /// Obtient une commande par son identifiant
    /// </summary>
    /// <param name="id">Identifiant de la commande</param>
    /// <returns>La commande trouvée ou null si aucune</returns>
    Task<Commande> GetByIdAsync(int id);

    /// <summary>
    /// Obtient toutes les commandes
    /// </summary>
    /// <returns>Collection de toutes les commandes</returns>
    Task<IEnumerable<Commande>> GetAllAsync();

    /// <summary>
    /// Obtient les commandes d'un client spécifique
    /// </summary>
    /// <param name="clientId">Identifiant du client</param>
    /// <returns>Collection des commandes du client</returns>
    Task<IEnumerable<Commande>> GetByClientAsync(int clientId);

    /// <summary>
    /// Obtient les commandes avec un statut spécifique
    /// </summary>
    /// <param name="statut">Statut recherché</param>
    /// <returns>Collection des commandes ayant le statut spécifié</returns>
    Task<IEnumerable<Commande>> GetByStatutAsync(StatutCommande statut);

    /// <summary>
    /// Ajoute une nouvelle commande
    /// </summary>
    /// <param name="commande">Commande à ajouter</param>
    /// <returns>Commande ajoutée avec son identifiant généré</returns>
    Task<Commande> AddAsync(Commande commande);

    /// <summary>
    /// Met à jour une commande existante
    /// </summary>
    /// <param name="commande">Commande avec les nouvelles valeurs</param>
    Task UpdateAsync(Commande commande);

    /// <summary>
    /// Supprime une commande par son identifiant
    /// </summary>
    /// <param name="id">Identifiant de la commande à supprimer</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Vérifie si une commande existe via son identifiant
    /// </summary>
    /// <param name="id">Identifiant de la commande</param>
    /// <returns>True si la commande existe, sinon False</returns>
    Task<bool> ExisteAsync(int id);
}