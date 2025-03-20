using FormationTestUnitaires.Entities;

namespace FormationTestUnitaires.Repositories;

/// <summary>
/// Interface définissant les opérations du repository pour les produits
/// </summary>
public interface IProduitRepository
{
    /// <summary>
    /// Obtient un produit par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <returns>Le produit trouvé ou null si aucun</returns>
    Task<Produit> GetByIdAsync(int id);

    /// <summary>
    /// Obtient tous les produits
    /// </summary>
    /// <returns>Collection de tous les produits</returns>
    Task<IEnumerable<Produit>> GetAllAsync();

    /// <summary>
    /// Obtient les produits d'une catégorie spécifique
    /// </summary>
    /// <param name="categorie">Catégorie recherchée</param>
    /// <returns>Collection des produits de la catégorie</returns>
    Task<IEnumerable<Produit>> GetByCategorieAsync(string categorie);

    /// <summary>
    /// Ajoute un nouveau produit
    /// </summary>
    /// <param name="produit">Produit à ajouter</param>
    /// <returns>Produit ajouté avec son identifiant généré</returns>
    Task<Produit> AddAsync(Produit produit);

    /// <summary>
    /// Met à jour un produit existant
    /// </summary>
    /// <param name="produit">Produit avec les nouvelles valeurs</param>
    Task UpdateAsync(Produit produit);

    /// <summary>
    /// Met à jour uniquement le stock d'un produit
    /// </summary>
    /// <param name="produitId">Identifiant du produit</param>
    /// <param name="nouvelleQuantite">Nouvelle quantité en stock</param>
    Task UpdateStockAsync(int produitId, int nouvelleQuantite);

    /// <summary>
    /// Supprime un produit par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du produit à supprimer</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Vérifie si un produit existe via son identifiant
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <returns>True si le produit existe, sinon False</returns>
    Task<bool> ExisteAsync(int id);
}
