using Moq;

namespace Exemples.TU.C_Mocks;

public class MockVerifyTest
{
    [Fact]
    public void DeleteUser_ExistingUser_DeletesUserAndSendsNotification()
    {
        // Arrange
        int userId = 1;
        var user = new User { Id = userId, Email = "test@example.com" };

        var mockRepository = new Mock<IUserRepository>();
        var mockNotification = new Mock<INotificationService>();

        // Configuration du mock pour retourner l'utilisateur quand GetById est appelé
        mockRepository.Setup(repo => repo.GetById(userId)).Returns(user);

        var userService = new UserService(mockRepository.Object, mockNotification.Object);

        // Act
        userService.DeleteUser(userId);

        // Assert avec Verify
        mockRepository.Verify(repo => repo.Delete(userId), Times.Once);
        mockNotification.Verify(notify => notify.NotifyUserDeleted(user.Email), Times.Once);    
    }
}