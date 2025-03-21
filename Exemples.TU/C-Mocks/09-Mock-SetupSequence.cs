using Moq;

namespace Exemples.TU.C_Mocks;

public class MockSetupSequenceTest
{
    [Fact]
    public void GetUserPermissions_WithSetupSequence_ReturnsDifferentResults()
    {
        // Arrange
        int userId = 1;
        var user = new User { Id = userId, Name = "Test User" };

        var firstPermissions = new List<string> { "read" };
        var secondPermissions = new List<string> { "read", "write" };
        var thirdPermissions = new List<string> { "read", "write", "admin" };

        var mockRepository = new Mock<IUserRepository>();
        var mockNotification = new Mock<INotificationService>();

        // Configuration pour que GetById retourne toujours le même utilisateur
        mockRepository.Setup(r => r.GetById(userId)).Returns(user);

        // Configuration de SetupSequence pour GetPermissions
        // Retourne des valeurs différentes à chaque appel
        mockRepository.SetupSequence(r => r.GetPermissions(userId))
            .Returns(firstPermissions)
            .Returns(secondPermissions)
            .Returns(thirdPermissions);

        var service = new UserService(
            mockRepository.Object,
            mockNotification.Object
        );

        // Act & Assert
        // Premier appel
        var result1 = service.GetUserPermissions(userId);
        Assert.Equal(firstPermissions, result1);

        // Deuxième appel
        var result2 = service.GetUserPermissions(userId);
        Assert.Equal(secondPermissions, result2);

        // Troisième appel
        var result3 = service.GetUserPermissions(userId);
        Assert.Equal(thirdPermissions, result3);
    }
}
