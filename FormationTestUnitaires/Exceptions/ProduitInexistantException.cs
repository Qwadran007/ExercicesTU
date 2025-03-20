namespace FormationTestUnitaires.Exceptions
{
    /// <summary>
    /// Exception levée lorsqu'un produit n'existe pas
    /// </summary>
    public class ProduitInexistantException : Exception
    {
        /// <summary>
        /// Identifiant du produit qui n'existe pas
        /// </summary>
        public int ProduitId { get; }

        /// <summary>
        /// Crée une nouvelle instance de l'exception ProduitInexistantException
        /// </summary>
        /// <param name="produitId">Identifiant du produit qui n'existe pas</param>
        public ProduitInexistantException(int produitId)
            : base($"Le produit avec l'ID {produitId} n'existe pas.")
        {
            ProduitId = produitId;
        }
    }
}
