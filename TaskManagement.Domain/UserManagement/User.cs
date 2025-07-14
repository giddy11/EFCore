using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.UserManagement;

public class User
{
    //protected User() { }

    public static User New(string email, string firstName, string lastName, AccountTypes accountType, Guid? Id = null)
    {
        return new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            AccountType = accountType,
            Id = Id ?? Guid.Empty
        };
    }

    public static User New(string email, string firstName, string lastName, AccountTypes? accountType = null, Guid? Id = null)
    {
        return new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            AccountType = AccountTypes.User,
            Id = Id ?? Guid.Empty
        };
    }

    public void Update(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public User SetPassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }

    public User ChangeUserStatus(UserStatus userStatus)
    {
        UserStatus = userStatus;
        return this;
    }

    public Guid Id { get; init; }
    public required string Email { get; set; }
    public string? PasswordHash { get; protected set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public AccountTypes AccountType { get; protected set; }
    public UserStatus UserStatus { get; protected set; } = UserStatus.Active;
}
