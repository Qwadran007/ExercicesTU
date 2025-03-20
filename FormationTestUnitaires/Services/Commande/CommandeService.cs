using FormationTestUnitaires.Entities;
using FormationTestUnitaires.Exceptions;
using FormationTestUnitaires.Repositories;

namespace FormationTestUnitaires.Services.PanierServices;

/// <summary>
/// Service gérant les opérations liées aux commandes
/// </summary>
public class CommandeService
{
    private readonly ICommandeRepository _commandeRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IProduitRepository _produitRepository;

    /// <summary>
    /// Constructeur du service commande
    /// </summary>
    /// <param name="commandeRepository">Repository d'accès aux données commandes</param>
    /// <param name="clientRepository">Repository d'accès aux données clients</param>
    /// <param name="produitRepository">Repository d'accès aux données produits</param>
    public CommandeService(
        ICommandeRepository commandeRepository,
        IClientRepository clientRepository,
        IProduitRepository produitRepository)
    {
        _commandeRepository = commandeRepository ?? throw new ArgumentNullException(nameof(commandeRepository));
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _produitRepository = produitRepository ?? throw new ArgumentNullException(nameof(produitRepository));
    }

    /// <summary>
    /// Obtient une commande par son identifiant
    /// </summary>
    /// <param name="id">Identifiant de la commande</param>
    /// <returns>La commande trouvée</returns>
    /// <exception cref="CommandeInexistanteException">Si la commande n'existe pas</exception>
    public async Task<Commande> ObtenirCommandeParIdAsync(int id)
    {
        var commande = await _commandeRepository.GetByIdAsync(id);

        if (commande == null)
            throw new CommandeInexistanteException(id);

        return commande;
    }

    /// <summary>
    /// Obtient toutes les commandes
    /// </summary>
    /// <returns>Liste de toutes les commandes</returns>
    public async Task<IEnumerable<Commande>> ObtenirToutesCommandesAsync()
    {
        return await _commandeRepository.GetAllAsync();
    }

    /// <summary>
    /// Obtient les commandes d'un client
    /// </summary>
    /// <param name="clientId">Identifiant du client</param>
    /// <returns>Liste des commandes du client</returns>
    /// <exception cref="ClientInexistantException">Si le client n'existe pas</exception>
    public async Task<IEnumerable<Commande>> ObtenirCommandesClientAsync(int clientId)
    {
        var clientExiste = await _clientRepository.ExisteAsync(clientId);
        if (!clientExiste)
            throw new ClientInexistantException(clientId);

        return await _commandeRepository.GetByClientAsync(clientId);
    }

    /// <summary>
    /// Crée une nouvelle commande pour un client
    /// </summary>
    /// <param name="clientId">Identifiant du client</param>
    /// <returns>La commande créée</returns>
    /// <exception cref="ClientInexistantException">Si le client n'existe pas</exception>
    public async Task<Commande> CreerCommandeAsync(int clientId)
    {
        var clientExiste = await _clientRepository.ExisteAsync(clientId);
        if (!clientExiste)
            throw new ClientInexistantException(clientId);

        var commande = new Commande
        {
            ClientId = clientId,
            DateCreation = DateTime.Now,
            Statut = StatutCommande.Brouillon,
            LignesCommande = new List<LigneCommande>()
        };

        return await _commandeRepository.AddAsync(commande);
    }

    /// <summary>
    /// Ajoute un produit à une commande
    /// </summary>
    /// <param name="commandeId">Identifiant de la commande</param>
    /// <param name="produitId">Identifiant du produit</param>
    /// <param name="quantite">Quantité à ajouter</param>
    /// <returns>La commande mise à jour</returns>
    /// <exception cref="CommandeInexistanteException">Si la commande n'existe pas</exception>
    /// <exception cref="ProduitInexistantException">Si le produit n'existe pas</exception>
    /// <exception cref="CommandeInvalideException">Si la commande ne peut plus être modifiée</exception>
    /// <exception cref="ArgumentException">Si la quantité est invalide</exception>
    /// <exception cref="StockInsuffisantException">Si le stock est insuffisant</exception>
    public async Task<Commande> AjouterProduitCommandeAsync(int commandeId, int produitId, int quantite)
    {
        if (quantite <= 0)
            throw new ArgumentException("La quantité doit être supérieure à zéro");

        var commande = await _commandeRepository.GetByIdAsync(commandeId);
        if (commande == null)
            throw new CommandeInexistanteException(commandeId);

        if (!commande.PeutEtreModifiee)
            throw new CommandeInvalideException(commandeId, "La commande ne peut plus être modifiée");

        var produit = await _produitRepository.GetByIdAsync(produitId);
        if (produit == null)
            throw new ProduitInexistantException(produitId);

        if (produit.StockDisponible < quantite)
            throw new StockInsuffisantException(produitId, quantite, produit.StockDisponible);

        // Vérifier si le produit est déjà dans la commande
        var ligneExistante = commande.LignesCommande.FirstOrDefault(l => l.ProduitId == produitId);
        if (ligneExistante != null)
        {
            // Mettre à jour la quantité
            ligneExistante.Quantite += quantite;
        }
        else
        {
            // Ajouter une nouvelle ligne
            commande.LignesCommande.Add(new LigneCommande
            {
                CommandeId = commandeId,
                ProduitId = produitId,
                Produit = produit,
                Quantite = quantite,
                PrixUnitaire = produit.Prix
            });
        }

        await _commandeRepository.UpdateAsync(commande);
        return commande;
    }

    /// <summary>
    /// Change le statut d'une commande
    /// </summary>
    /// <param name="commandeId">Identifiant de la commande</param>
    /// <param name="nouveauStatut">Nouveau statut de la commande</param>
    /// <returns>La commande mise à jour</returns>
    /// <exception cref="CommandeInexistanteException">Si la commande n'existe pas</exception>
    /// <exception cref="CommandeInvalideException">Si le changement de statut est invalide</exception>
    public async Task<Commande> ChangerStatutCommandeAsync(int commandeId, StatutCommande nouveauStatut)
    {
        var commande = await _commandeRepository.GetByIdAsync(commandeId);
        if (commande == null)
            throw new CommandeInexistanteException(commandeId);

        // Vérifier si le changement de statut est valide
        if (!EstChangementStatutValide(commande.Statut, nouveauStatut))
            throw new CommandeInvalideException(commandeId, $"Changement de statut invalide: {commande.Statut} vers {nouveauStatut}");

        // Si on confirme la commande, vérifier qu'elle contient au moins une ligne
        if (nouveauStatut == StatutCommande.Confirmee && !commande.LignesCommande.Any())
            throw new CommandeInvalideException(commandeId, "La commande doit contenir au moins un produit pour être confirmée");

        commande.Statut = nouveauStatut;

        // Si la commande est expédiée, définir la date de livraison estimée
        if (nouveauStatut == StatutCommande.Expediee && !commande.DateLivraison.HasValue)
            commande.DateLivraison = DateTime.Now.AddDays(3);  // Estimation de livraison dans 3 jours

        // Si la commande est livrée, mettre à jour la date de livraison effective
        if (nouveauStatut == StatutCommande.Livree)
            commande.DateLivraison = DateTime.Now;

        await _commandeRepository.UpdateAsync(commande);
        return commande;
    }

    /// <summary>
    /// Supprime une commande par son identifiant (uniquement si elle est en brouillon)
    /// </summary>
    /// <param name="id">Identifiant de la commande à supprimer</param>
    /// <returns>Tâche asynchrone</returns>
    /// <exception cref="CommandeInexistanteException">Si la commande n'existe pas</exception>
    /// <exception cref="CommandeInvalideException">Si la commande ne peut pas être supprimée</exception>
    public async Task SupprimerCommandeAsync(int id)
    {
        var commande = await _commandeRepository.GetByIdAsync(id);
        if (commande == null)
            throw new CommandeInexistanteException(id);

        if (commande.Statut != StatutCommande.Brouillon)
            throw new CommandeInvalideException(id, "Seules les commandes en brouillon peuvent être supprimées");

        await _commandeRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Vérifie si un changement de statut est valide
    /// </summary>
    /// <param name="statutActuel">Statut actuel de la commande</param>
    /// <param name="nouveauStatut">Nouveau statut souhaité</param>
    /// <returns>True si le changement est valide, sinon False</returns>
    private bool EstChangementStatutValide(StatutCommande statutActuel, StatutCommande nouveauStatut)
    {
        // Règles de progression des statuts
        switch (statutActuel)
        {
            case StatutCommande.Brouillon:
                return nouveauStatut == StatutCommande.EnAttente || nouveauStatut == StatutCommande.Annulee;

            case StatutCommande.EnAttente:
                return nouveauStatut == StatutCommande.Confirmee || nouveauStatut == StatutCommande.Annulee;

            case StatutCommande.Confirmee:
                return nouveauStatut == StatutCommande.EnPreparation || nouveauStatut == StatutCommande.Annulee;

            case StatutCommande.EnPreparation:
                return nouveauStatut == StatutCommande.Expediee || nouveauStatut == StatutCommande.Annulee;

            case StatutCommande.Expediee:
                return nouveauStatut == StatutCommande.Livree;

            case StatutCommande.Livree:
            case StatutCommande.Annulee:
                return false; // Statuts finaux

            default:
                return false;
        }
    }
}