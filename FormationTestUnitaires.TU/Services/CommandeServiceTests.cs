namespace FormationTestUnitaires.Tests.Services;

public class CommandeServiceTests
{
    public async Task CreerCommande_VerifieProprietesCommandeCreee()
    {
        // Arrange

        // Act
     
        // Vérifie que la date de création est récente (moins de 2 secondes)
    }

    public async Task AjouterProduitCommande_QuantitesVariees_AjouteLaBonneQuantite()
    {
        // Arrange
      
        // Act

        // Assert
    }

    public async Task ChangerStatutCommande_CommandeLivree_MetAJourDateEffective()
    {
        // Arrange

        // Act

        // Assert
        // Vérifie que la date prévue a été remplacée par la date effective (aujourd'hui)
        // Vérifie que la mise à jour a été effectuée exactement une fois
    }

    public async Task AjouterProduitCommande_VerifieSequenceAppels()
    {
        // Arrange
     
        // Act
       
        // Assert
    }
}
