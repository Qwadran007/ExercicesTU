using Moq;
using Moq.Protected;

namespace Introduction.C_Mocks;

public class MockProtectedTest
{
    public class ProtectedServiceTests
    {
        [Fact]
        public void TestProtectedMethod()
        {
            // Arrange
            var mockService = new Mock<BaseUserService>();

            // Configuration d'une méthode protégée
            mockService.Protected()
                .Setup<string>("FormatUserName", ItExpr.IsAny<string>())
                .Returns("FORMATTED_NAME");

            // Act
            var result = mockService.Object.GetFormattedUserName("John");

            // Assert
            Assert.Equal("FORMATTED_NAME", result);
        }
    }

    // Classe de base avec méthode protégée
    public abstract class BaseUserService
    {
        // Méthode protégée que nous voulons mocker
        protected virtual string FormatUserName(string name)
        {
            return name.ToUpper();
        }

        // Méthode publique qui utilise la méthode protégée
        public string GetFormattedUserName(string name)
        {
            return FormatUserName(name);
        }
    }
}


/*
 * Permet de :
 * 
 * Configurer des méthodes protégées pour qu'elles retournent des valeurs spécifiques
 * Vérifier si elles ont été appelées et avec quels paramètres
 * Remplacer leur implémentation par défaut
 * 
 * 
 * ATTENTION : 
 * Seules les méthodes protégées virtuelles ou abstraites peuvent être mockées
 * Les méthodes doivent être référencées par leur nom sous forme de chaîne de caractères
 * Utiliser ItExpr au lieu de It pour configurer les expressions de paramètres
 * 
 * 
 */