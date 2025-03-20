using FormationTestUnitaires.Entities;
using FormationTestUnitaires.Exceptions;
using FormationTestUnitaires.Repositories;

namespace FormationTestUnitaires.Services.ProduitServices;

/// <summary>
/// Service gérant les opérations liées aux produits
/// </summary>
public class ProduitService
{
    private readonly IProduitRepository _produitRepository;

    /// <summary>
    /// Constructeur du service produit
    /// </summary>
    /// <param name="produitRepository">Repository d'accès aux données produits</param>
    public ProduitService(IProduitRepository produitRepository)
    {
        _produitRepository = produitRepository ?? throw new ArgumentNullException(nameof(produitRepository));
    }

    /// <summary>
    /// Obtient un produit par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <returns>Le produit trouvé</returns>
    /// <exception cref="ProduitInexistantException">Si le produit n'existe pas</exception>
    public async Task<Produit> ObtenirProduitParIdAsync(int id)
    {
        var produit = await _produitRepository.GetByIdAsync(id);

        if (produit == null)
            throw new ProduitInexistantException(id);

        return produit;
    }

    /// <summary>
    /// Obtient tous les produits
    /// </summary>
    /// <returns>Liste de tous les produits</returns>
    public async Task<IEnumerable<Produit>> ObtenirTousProduitsAsync()
    {
        return await _produitRepository.GetAllAsync();
    }

    /// <summary>
    /// Obtient les produits par catégorie
    /// </summary>
    /// <param name="categorie">Catégorie des produits à rechercher</param>
    /// <returns>Liste des produits de la catégorie spécifiée</returns>
    public async Task<IEnumerable<Produit>> ObtenirProduitsParCategorieAsync(string categorie)
    {
        if (string.IsNullOrWhiteSpace(categorie))
            throw new ArgumentException("La catégorie ne peut pas être vide");

        return await _produitRepository.GetByCategorieAsync(categorie);
    }

    /// <summary>
    /// Crée un nouveau produit
    /// </summary>
    /// <param name="produit">Le produit à créer</param>
    /// <returns>Le produit créé avec son Id généré</returns>
    /// <exception cref="ArgumentException">Si les données du produit sont invalides</exception>
    public async Task<Produit> CreerProduitAsync(Produit produit)
    {
        // Validation des données du produit
        if (produit == null)
            throw new ArgumentNullException(nameof(produit));

        if (string.IsNullOrWhiteSpace(produit.Nom))
            throw new ArgumentException("Le nom du produit est obligatoire");

        if (produit.Prix <= 0)
            throw new ArgumentException("Le prix doit être supérieur à zéro");

        if (produit.StockDisponible < 0)
            throw new ArgumentException("Le stock ne peut pas être négatif");

        return await _produitRepository.AddAsync(produit);
    }

    /// <summary>
    /// Met à jour les informations d'un produit existant
    /// </summary>
    /// <param name="produit">Le produit avec les informations mises à jour</param>
    /// <returns>Tâche asynchrone</returns>
    /// <exception cref="ProduitInexistantException">Si le produit n'existe pas</exception>
    /// <exception cref="ArgumentException">Si les données du produit sont invalides</exception>
    public async Task MettreAJourProduitAsync(Produit produit)
    {
        if (produit == null)
            throw new ArgumentNullException(nameof(produit));

        if (string.IsNullOrWhiteSpace(produit.Nom))
            throw new ArgumentException("Le nom du produit est obligatoire");

        if (produit.Prix <= 0)
            throw new ArgumentException("Le prix doit être supérieur à zéro");

        if (produit.StockDisponible < 0)
            throw new ArgumentException("Le stock ne peut pas être négatif");

        var existant = await _produitRepository.ExisteAsync(produit.Id);
        if (!existant)
            throw new ProduitInexistantException(produit.Id);

        await _produitRepository.UpdateAsync(produit);
    }

    /// <summary>
    /// Met à jour le stock d'un produit
    /// </summary>
    /// <param name="produitId">Identifiant du produit</param>
    /// <param name="nouvelleQuantite">Nouvelle quantité de stock</param>
    /// <returns>Tâche asynchrone</returns>
    /// <exception cref="ProduitInexistantException">Si le produit n'existe pas</exception>
    /// <exception cref="ArgumentException">Si la nouvelle quantité est négative</exception>
    public async Task MettreAJourStockAsync(int produitId, int nouvelleQuantite)
    {
        if (nouvelleQuantite < 0)
            throw new ArgumentException("Le stock ne peut pas être négatif");

        var existant = await _produitRepository.ExisteAsync(produitId);
        if (!existant)
            throw new ProduitInexistantException(produitId);

        await _produitRepository.UpdateStockAsync(produitId, nouvelleQuantite);
    }

    /// <summary>
    /// Supprime un produit par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du produit à supprimer</param>
    /// <returns>Tâche asynchrone</returns>
    /// <exception cref="ProduitInexistantException">Si le produit n'existe pas</exception>
    public async Task SupprimerProduitAsync(int id)
    {
        var existant = await _produitRepository.ExisteAsync(id);
        if (!existant)
            throw new ProduitInexistantException(id);

        await _produitRepository.DeleteAsync(id);
    }
}