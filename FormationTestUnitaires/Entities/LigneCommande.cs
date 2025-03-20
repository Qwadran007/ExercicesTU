namespace FormationTestUnitaires.Entities;

/// <summary>
/// Représente une ligne de commande (un produit dans une commande)
/// </summary>
public class LigneCommande
{
    /// <summary>
    /// Identifiant unique de la ligne de commande
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifiant de la commande associée
    /// </summary>
    public int CommandeId { get; set; }

    /// <summary>
    /// Identifiant du produit commandé
    /// </summary>
    public int ProduitId { get; set; }

    /// <summary>
    /// Référence vers le produit associé à cette ligne
    /// </summary>
    public Produit Produit { get; set; }

    /// <summary>
    /// Quantité commandée du produit
    /// </summary>
    public int Quantite { get; set; }

    /// <summary>
    /// Prix unitaire du produit au moment de la commande
    /// </summary>
    public decimal PrixUnitaire { get; set; }

    /// <summary>
    /// Calcule le total de la ligne (prix unitaire × quantité)
    /// </summary>
    public decimal Total => PrixUnitaire * Quantite;
}