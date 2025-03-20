namespace FormationTestUnitaires.Exceptions
{
    /// <summary>
    /// Exception levée lorsqu'une commande est dans un état invalide ou qu'une opération sur une commande est invalide
    /// </summary>
    public class CommandeInvalideException : Exception
    {
        /// <summary>
        /// Identifiant de la commande invalide
        /// </summary>
        public int CommandeId { get; }

        /// <summary>
        /// Raison de l'invalidité de la commande
        /// </summary>
        public string Raison { get; }

        /// <summary>
        /// Crée une nouvelle instance de l'exception CommandeInvalideException
        /// </summary>
        /// <param name="commandeId">Identifiant de la commande invalide</param>
        /// <param name="raison">Raison de l'invalidité</param>
        public CommandeInvalideException(int commandeId, string raison)
            : base($"La commande avec l'ID {commandeId} est invalide. Raison : {raison}")
        {
            CommandeId = commandeId;
            Raison = raison;
        }
    }
}
