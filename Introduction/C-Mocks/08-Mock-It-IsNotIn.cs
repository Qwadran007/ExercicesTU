using Moq;

namespace Introduction.C_Mocks;

public class MockItIsNotInTest
{
    [Fact]
    public void GetUserById_WithItIsNotIn_ReturnsExpectedUser()
    {
        // Arrange
        var expectedUser = new User { Id = 5, Name = "Test User" };
        var blockedIds = new[] { 1, 2, 3 }; // IDs à exclure

        var mockRepository = new Mock<IUserRepository>();
        var mockNotification = new Mock<INotificationService>();

        // Configuration du mock repository avec It.IsNotIn
        // Ne retourne l'utilisateur que si l'ID n'est pas dans la liste des IDs bloqués
        mockRepository.Setup(repo => repo.GetById(It.IsNotIn(blockedIds)))
            .Returns(expectedUser);

        var service = new UserService(mockRepository.Object, mockNotification.Object);

        // Act
        var result = service.GetUserById(5); // ID qui n'est pas dans la liste bloquée

        // Assert
        Assert.Equal(expectedUser.Name, result.Name);
    }
}
