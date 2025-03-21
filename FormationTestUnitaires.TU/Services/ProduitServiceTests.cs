using FormationTestUnitaires.Entities;
using FormationTestUnitaires.Exceptions;
using Moq;

namespace FormationTestUnitaires.TU.Unit.Services;

public class ProduitServiceTests
{
    public async Task ObtenirProduitsParCategorie_CategorieExiste_RetourneProduitsDeCategorie()
    {
        // Arrange
        string categorie = "Électronique";
        var produitsAttendus = new List<Produit>
            {
                new Produit { Id = 1, Nom = "Téléphone", Prix = 299.99m, StockDisponible = 5, Categorie = "Électronique" },
                new Produit { Id = 2, Nom = "Ordinateur", Prix = 999.99m, StockDisponible = 3, Categorie = "Électronique" }
            };

        // Act

        // Assert
    }
}