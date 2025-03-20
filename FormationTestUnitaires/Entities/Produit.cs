namespace FormationTestUnitaires.Entities;

/// <summary>
/// Représente un produit dans le système de gestion des commandes
/// </summary>
public class Produit
{
    /// <summary>
    /// Identifiant unique du produit
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom du produit
    /// </summary>
    public string Nom { get; set; }

    /// <summary>
    /// Description détaillée du produit
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Prix unitaire du produit
    /// </summary>
    public decimal Prix { get; set; }

    /// <summary>
    /// Quantité disponible en stock
    /// </summary>
    public int StockDisponible { get; set; }

    /// <summary>
    /// Catégorie du produit
    /// </summary>
    public string Categorie { get; set; }

    /// <summary>
    /// Indique si le produit est disponible à la vente
    /// (Un produit est disponible s'il y a du stock)
    /// </summary>
    public bool EstDisponible => StockDisponible > 0;
}