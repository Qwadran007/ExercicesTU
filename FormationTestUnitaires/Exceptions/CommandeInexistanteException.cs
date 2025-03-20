namespace FormationTestUnitaires.Exceptions
{
    /// <summary>
    /// Exception levée lorsqu'une commande n'existe pas
    /// </summary>
    public class CommandeInexistanteException : Exception
    {
        /// <summary>
        /// Identifiant de la commande qui n'existe pas
        /// </summary>
        public int CommandeId { get; }

        /// <summary>
        /// Crée une nouvelle instance de l'exception CommandeInexistanteException
        /// </summary>
        /// <param name="commandeId">Identifiant de la commande qui n'existe pas</param>
        public CommandeInexistanteException(int commandeId)
            : base($"La commande avec l'ID {commandeId} n'existe pas.")
        {
            CommandeId = commandeId;
        }
    }
}
