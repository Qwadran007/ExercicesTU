using Moq;

namespace Introduction.C_Mocks;

public class MockCallbackTest
{
    [Fact]
    public void UpdateUserStatus_ShouldModifyUser_WhenCalled()
    {
        // Arrange
        int userId = 1;
        bool newStatus = true;
        string reason = "Account activated";

        var user = new User
        {
            Id = userId,
            Email = "test@example.com",
            IsActive = false,
            StatusReason = null
        };

        var mockRepository = new Mock<IUserRepository>();
        var mockNotificationService = new Mock<INotificationService>();

        User capturedUser = null;

        mockRepository.Setup(repo => repo.GetById(userId)).Returns(user);

        mockRepository.Setup(repo => repo.Update(It.IsAny<User>()))
            .Callback<User>(u => capturedUser = u)
            .Returns(true);

        var userService = new UserService(mockRepository.Object, mockNotificationService.Object);

        // Act
        bool result = userService.UpdateUserStatus(userId, newStatus, reason);

        // Assert
        Assert.True(result);
        Assert.NotNull(capturedUser);
        Assert.Equal(newStatus, capturedUser.IsActive);
        Assert.Equal(reason, capturedUser.StatusReason);
    }
}