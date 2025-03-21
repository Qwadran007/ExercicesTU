using System;
using System.Collections.Generic;
using Xunit;

namespace Exemples.TU.B_OrganisationEtPartage;

// 1. Fixture qui simule une base de données en mémoire
public class InMemoryDataFixture : IDisposable
{
    private Dictionary<int, User> _users;

    public InMemoryDataFixture()
    {
        // Initialisation des données de test en mémoire
        _users = new Dictionary<int, User>
        {
            { 1, new User { Id = 1, Name = "Alice" } },
            { 2, new User { Id = 2, Name = "Bob" } }
        };
    }

    public Dictionary<int, User> Users => _users;

    public void Dispose()
    {
        // Nettoyage des ressources si nécessaire
        _users.Clear();
    }
}

// 2. Classe de tests implémentant IClassFixture
public class UserServiceTests : IClassFixture<InMemoryDataFixture>
{
    private readonly InMemoryDataFixture _fixture;

    public UserServiceTests(InMemoryDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void GetUserById_ExistingUser_ReturnsUser()
    {
        // Arrange
        var userService = new UserService(_fixture.Users);

        // Act
        var user = userService.GetUserById(1);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(1, user.Id);
        Assert.Equal("Alice", user.Name);
    }

    [Fact]
    public void GetUserById_NonExistingUser_ReturnsNull()
    {
        // Arrange
        var userService = new UserService(_fixture.Users);

        // Act
        var user = userService.GetUserById(999);

        // Assert
        Assert.Null(user);
    }
}

// Service utilisateur simplifié qui fonctionne avec un dictionnaire en mémoire
public class UserService
{
    private readonly Dictionary<int, User> _users;

    public UserService(Dictionary<int, User> users)
    {
        _users = users;
    }

    public User GetUserById(int id)
    {
        if (_users.TryGetValue(id, out var user))
        {
            return user;
        }
        return null;
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}