using FormationTestUnitaires.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormationTestUnitaires.Repositories.Data;

/// <summary>
/// Contexte de base de données pour l'application de gestion des commandes
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Constructeur principal
    /// </summary>
    /// <param name="options">Options de configuration du contexte</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Table des clients
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Table des produits
    /// </summary>
    public DbSet<Produit> Produits { get; set; }

    /// <summary>
    /// Table des commandes
    /// </summary>
    public DbSet<Commande> Commandes { get; set; }

    /// <summary>
    /// Table des lignes de commande
    /// </summary>
    public DbSet<LigneCommande> LignesCommande { get; set; }

    /// <summary>
    /// Configure les relations entre les entités et autres paramètres du modèle
    /// </summary>
    /// <param name="modelBuilder">Builder pour définir le modèle</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
            throw new ArgumentNullException(nameof(modelBuilder));

        // Configuration de l'entité Client
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Telephone).HasMaxLength(20);
            entity.Property(e => e.Adresse).HasMaxLength(500);
        });

        // Configuration de l'entité Produit
        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Prix).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Categorie).HasMaxLength(100);
            // La propriété EstDisponible est calculée et n'est pas stockée
            entity.Ignore(e => e.EstDisponible);
        });

        // Configuration de l'entité Commande
        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DateCreation).IsRequired();
            entity.Property(e => e.FraisLivraison).HasColumnType("decimal(18,2)");

            // Relation avec Client (many-to-one)
            entity.HasOne(c => c.Client)
                  .WithMany()
                  .HasForeignKey(c => c.ClientId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Propriétés calculées qui ne sont pas stockées
            entity.Ignore(e => e.SousTotal);
            entity.Ignore(e => e.Total);
            entity.Ignore(e => e.PeutEtreModifiee);
        });

        // Configuration de l'entité LigneCommande
        modelBuilder.Entity<LigneCommande>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Quantite).IsRequired();
            entity.Property(e => e.PrixUnitaire).HasColumnType("decimal(18,2)");

            // Relation avec Commande (many-to-one)
            entity.HasOne<Commande>()
                  .WithMany(c => c.LignesCommande)
                  .HasForeignKey(l => l.CommandeId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relation avec Produit (many-to-one)
            entity.HasOne(l => l.Produit)
                  .WithMany()
                  .HasForeignKey(l => l.ProduitId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Propriété calculée qui n'est pas stockée
            entity.Ignore(e => e.Total);
        });
    }
}