namespace FormationTestUnitaires.Exceptions
{
    public class StockInsuffisantException : Exception
    {
        /// <summary>
        /// Identifiant du produit invalide
        /// </summary>
        public int ProduitId { get; }

        /// <summary>
        /// Quantité demandée
        /// </summary>
        public int QuantiteDemandee { get; }

        /// <summary>
        /// Quantité disponible
        /// </summary>
        public int QuantiteDisponible { get; }

        /// <summary>
        /// Crée une nouvelle instance de l'exception StockInsuffisantException
        /// </summary>
        /// <param name="produitId">Identifiant du produit invalide</param>
        /// <param name="quantite">Quantité demandée</param>
        /// <param name="stockDisponible">Quantité disponible</param>
        public StockInsuffisantException(int produitId, int quantite, int stockDisponible)
            : base($"Le produit avec l'ID {produitId} n'a pas le stock demandé (demandé:{quantite}, stock:{stockDisponible}).")
        {
            ProduitId = produitId;
            QuantiteDemandee = quantite;
            QuantiteDisponible = stockDisponible;
        }
    }
}
