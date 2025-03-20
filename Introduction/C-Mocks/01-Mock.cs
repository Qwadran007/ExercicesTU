using Moq;

namespace Introduction.C_Mocks;

public class MockTest
{
    [Fact]
    public void GetUserById_ReturnsCorrectUser()
    {
        // Arrange
        int userId = 1;
        var expectedUser = new User { Id = userId, Name = "Test User" };

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(r => r.GetById(userId)).Returns(expectedUser);

        var service = new UserService(
            mockRepository.Object,
            new Mock<INotificationService>().Object
        );

        // Act
        var result = service.GetUserById(userId);

        // Assert
        Assert.Equal(expectedUser.Name, result.Name);
        Assert.Equal(expectedUser.Id, result.Id);
    }
}