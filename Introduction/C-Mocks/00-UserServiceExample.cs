namespace Introduction.C_Mocks;

/// <summary>
/// Service de gestion des utilisateurs simplifié pour démontrer les fonctionnalités de Moq.
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly INotificationService _notificationService;

    public UserService(IUserRepository repository, INotificationService notificationService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
    }

    // Méthode simple retournant une valeur - pour Setup/Returns
    public User GetUserById(int id)
    {
        return _repository.GetById(id);
    }

    // Méthode avec paramètre out - pour tester Setup avec out params
    public bool TryGetUserById(int id, out User user)
    {
        return _repository.TryGetById(id, out user);
    }

    // Méthode void - pour tester Verify sans retour
    public void DeleteUser(int id)
    {
        var user = _repository.GetById(id);
        if (user != null)
        {
            _repository.Delete(id);
            _notificationService.NotifyUserDeleted(user.Email);
        }
    }

    // Méthode async - pour tester avec ReturnsAsync
    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _repository.GetUsersAsync(u => u.IsActive);
    }

    // Méthode avec exception - pour tester Throws
    public User CreateUser(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var existing = _repository.GetByEmail(user.Email);
        if (existing != null)
            throw new DuplicateUserException("Cet email est déjà utilisé");

        var result = _repository.Create(user);
        _notificationService.NotifyUserCreated(user.Email);
        return result;
    }

    // Méthode avec paramètres multiples - pour tester It.IsAny, It.Is<T>
    public bool UpdateUserStatus(int userId, bool isActive, string reason)
    {
        var user = _repository.GetById(userId);
        if (user == null)
            return false;

        user.IsActive = isActive;
        user.StatusReason = reason;

        var success = _repository.Update(user);
        if (success)
        {
            _notificationService.NotifyStatusChanged(user.Email, isActive, reason);
        }

        return success;
    }

    // Méthode avec plusieurs appels - pour tester Callback et séquences
    public int ProcessUsers(IEnumerable<int> userIds)
    {
        int processedCount = 0;

        foreach (var id in userIds)
        {
            var user = _repository.GetById(id);
            if (user != null)
            {
                _repository.Process(user);
                processedCount++;
            }
        }

        return processedCount;
    }

    // Méthode avec ref param - pour tester Setup avec ref params
    public bool UpdateUserDetails(int id, ref UserDetails details)
    {
        return _repository.UpdateDetails(id, ref details);
    }

    // Méthode retournant différentes valeurs - pour tester SetupSequence
    public List<string> GetUserPermissions(int userId)
    {
        var user = _repository.GetById(userId);
        if (user == null)
            return new List<string>();

        return _repository.GetPermissions(userId);
    }
}

public class DuplicateUserException : Exception
{
    public DuplicateUserException(string message) : base(message) { }
}

// Interfaces simplifiées

public interface IUserService
{
    User GetUserById(int id);
    bool TryGetUserById(int id, out User user);
    void DeleteUser(int id);
    Task<IEnumerable<User>> GetActiveUsersAsync();
    User CreateUser(User user);
    bool UpdateUserStatus(int userId, bool isActive, string reason);
    int ProcessUsers(IEnumerable<int> userIds);
    bool UpdateUserDetails(int id, ref UserDetails details);
    List<string> GetUserPermissions(int userId);
}

public interface IUserRepository
{
    User GetById(int id);
    bool TryGetById(int id, out User user);
    User GetByEmail(string email);
    User Create(User user);
    bool Update(User user);
    void Delete(int id);
    void Process(User user);
    Task<IEnumerable<User>> GetUsersAsync(Func<User, bool> filter);
    bool UpdateDetails(int id, ref UserDetails details);
    List<string> GetPermissions(int userId);
}

public interface INotificationService
{
    void NotifyUserCreated(string email);
    void NotifyUserDeleted(string email);
    void NotifyStatusChanged(string email, bool isActive, string reason);
}

// Classes de données simplifiées

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string StatusReason { get; set; }
}

public class UserDetails
{
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}