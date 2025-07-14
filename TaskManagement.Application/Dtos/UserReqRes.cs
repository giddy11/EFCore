using System.ComponentModel.DataAnnotations;
using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Application.Dtos;

public class CreateUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string LastName { get; set; } = string.Empty;

    //[Required]
    //public AccountTypes AccountType { get; set; }

    //[StringLength(100, MinimumLength = 6)]
    //public string? Password { get; set; }
}

public class CreateUserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public AccountTypes AccountType { get; set; }
    public UserStatus UserStatus { get; set; }
}

public class GetUserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public AccountTypes AccountType { get; set; }
    public UserStatus UserStatus { get; set; }
}

public class UpdateUserRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public AccountTypes AccountType { get; set; }
    public UserStatus UserStatus { get; set; }
}