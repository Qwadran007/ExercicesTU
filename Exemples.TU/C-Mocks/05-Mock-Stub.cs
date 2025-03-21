using Exemples.TU.B_OrganisationEtPartage;
using Moq;

namespace Exemples.TU.C_Mocks;

public class MockStubTest
{
    [Fact]
    public void GetUserPermissions_ReturnsCorrectPermissions()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Test User" };
        var expectedPermissions = new List<string> { "read", "write", "admin" };

        // Création d'un stub avec Moq
        var stubRepository = new Mock<IUserRepository>();

        // Configuration du stub pour simuler un utilisateur existant
        stubRepository.Setup(r => r.GetById(user.Id)).Returns(user);

        // Configuration du stub pour retourner des permissions prédéfinies
        stubRepository.Setup(r => r.GetPermissions(user.Id))
            .Returns(expectedPermissions);

        var service = new UserService(
            stubRepository.Object,
            new Mock<INotificationService>().Object
        );

        // Act
        var result = service.GetUserPermissions(user.Id);

        // Assert
        Assert.Equal(expectedPermissions, result);
    }
}
