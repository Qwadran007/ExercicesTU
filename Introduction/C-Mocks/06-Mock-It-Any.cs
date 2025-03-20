using Moq;

namespace Introduction.C_Mocks;

public class MockItAnyTest
{
    [Fact]
    public void GetUserById_WithItIsAny_ReturnsExpectedUser()
    {
        // Arrange
        var expectedUserName = "Test User";
        var expectedUser = new User { Id = 42, Name = expectedUserName };

        var mockRepository = new Mock<IUserRepository>();
        var mockNotification = new Mock<INotificationService>();

        // Configuration du mock repository
        // GetById est la méthode du repository qui est appelée par GetUserById du service
        mockRepository.Setup(repo => repo.GetById(It.IsAny<int>()))
            .Returns(expectedUser);

        var service = new UserService(mockRepository.Object, mockNotification.Object);

        // Act
        // Appel de la méthode du service qui utilise le repository mocké
        var result = service.GetUserById(123);

        // Assert
        Assert.Equal(expectedUserName, result.Name);
    }
}
