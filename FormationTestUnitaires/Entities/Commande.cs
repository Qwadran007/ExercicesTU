namespace FormationTestUnitaires.Entities;

/// <summary>
/// Statuts possibles d'une commande
/// </summary>
public enum StatutCommande
{
    /// <summary>
    /// Commande au stade initial, encore modifiable
    /// </summary>
    Brouillon,

    /// <summary>
    /// Commande validée par le client, en attente de traitement
    /// </summary>
    EnAttente,

    /// <summary>
    /// Commande confirmée, prête à être préparée
    /// </summary>
    Confirmee,

    /// <summary>
    /// Commande en cours de préparation
    /// </summary>
    EnPreparation,

    /// <summary>
    /// Commande expédiée au client
    /// </summary>
    Expediee,

    /// <summary>
    /// Commande livrée au client
    /// </summary>
    Livree,

    /// <summary>
    /// Commande annulée
    /// </summary>
    Annulee
}

/// <summary>
/// Représente une commande dans le système de gestion des commandes
/// </summary>
public class Commande
{
    /// <summary>
    /// Identifiant unique de la commande
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifiant du client ayant passé la commande
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Référence vers le client associé à la commande
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// Date de création de la commande
    /// </summary>
    public DateTime DateCreation { get; set; }

    /// <summary>
    /// Date de livraison prévue ou effective de la commande
    /// </summary>
    public DateTime? DateLivraison { get; set; }

    /// <summary>
    /// Liste des lignes de commande (produits commandés)
    /// </summary>
    public List<LigneCommande> LignesCommande { get; set; } = new List<LigneCommande>();

    /// <summary>
    /// Statut actuel de la commande
    /// </summary>
    public StatutCommande Statut { get; set; }

    /// <summary>
    /// Frais de livraison appliqués à la commande
    /// </summary>
    public decimal FraisLivraison { get; set; }

    /// <summary>
    /// Calcule le sous-total de la commande (somme des lignes)
    /// </summary>
    public decimal SousTotal => LignesCommande.Sum(l => l.Total);

    /// <summary>
    /// Calcule le montant total de la commande (sous-total + frais de livraison)
    /// </summary>
    public decimal Total => SousTotal + FraisLivraison;

    /// <summary>
    /// Indique si la commande peut encore être modifiée
    /// </summary>
    public bool PeutEtreModifiee =>
        Statut == StatutCommande.Brouillon ||
        Statut == StatutCommande.EnAttente;
}