namespace FormationTestUnitaires.Entities;

/// <summary>
/// Représente un client dans le système de gestion des commandes
/// </summary>
public class Client
{
    /// <summary>
    /// Identifiant unique du client
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom complet du client
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Adresse email du client
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Numéro de téléphone du client
    /// </summary>
    public string Telephone { get; set; } = string.Empty;

    /// <summary>
    /// Adresse postale du client
    /// </summary>
    public string Adresse { get; set; } = string.Empty;

    /// <summary>
    /// Date d'inscription du client
    /// </summary>
    public DateTime DateInscription { get; set; }

    /// <summary>
    /// Indique si le client bénéficie du statut premium
    /// </summary>
    public bool EstPremium { get; set; }
}