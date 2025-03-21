using Moq;

namespace Exemples.TU.C_Mocks;

public class MockItIsTest
{
    [Fact]
    public void GetUserById_WithItIs_ReturnsExpectedUser()
    {
        // Arrange
        var expectedUser = new User { Id = 1, Name = "Test User" };

        var mockRepository = new Mock<IUserRepository>();
        var mockNotification = new Mock<INotificationService>();

        // Configuration du mock repository avec It.Is<T>
        // Ne retourne l'utilisateur que si l'ID est positif
        mockRepository.Setup(repo => repo.GetById(It.Is<int>(id => id > 0)))
            .Returns(expectedUser);

        var service = new UserService(mockRepository.Object, mockNotification.Object);

        // Act
        var result = service.GetUserById(1); // ID positif

        // Assert
        Assert.Equal(expectedUser.Name, result.Name);
    }
}
