using Moq;

namespace Exemples.TU.C_Mocks;

public class MockSetupTest
{
    [Fact]
    public void GetUserById_ExistingId_ReturnsUser()
    {
        // Arrange
        int userId = 1;
        var expectedUser = new User { Id = userId, Name = "Test User", Email = "test@example.com" };

        var mockRepository = new Mock<IUserRepository>();
        var mockNotificationService = new Mock<INotificationService>();

        mockRepository.Setup(repo => repo.GetById(userId)).Returns(expectedUser);

        var userService = new UserService(mockRepository.Object, mockNotificationService.Object);

        // Act
        var result = userService.GetUserById(userId);

        // Assert
        Assert.Equal(expectedUser, result);
    }
}
