namespace FormationTestUnitaires.Exceptions
{
    /// <summary>
    /// Exception levée lorsqu'un client n'existe pas
    /// </summary>
    public class ClientInexistantException : Exception
    {
        /// <summary>
        /// Identifiant du client qui n'existe pas
        /// </summary>
        public int ClientId { get; }

        /// <summary>
        /// Crée une nouvelle instance de l'exception ClientInexistantException
        /// </summary>
        /// <param name="clientId">Identifiant du client qui n'existe pas</param>
        public ClientInexistantException(int clientId)
            : base($"Le client avec l'ID {clientId} n'existe pas.")
        {
            ClientId = clientId;
        }
    }
}
