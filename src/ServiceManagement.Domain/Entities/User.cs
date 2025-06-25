using ServiceManagement.Domain.Enums;
using ServiceManagement.Domain.Errors;
using ServiceManagement.Domain.Exceptions;

namespace ServiceManagement.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Name { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; set; }
    public Company? Company { get; set; }

    protected User(){ }

    public User(string email, string passwordHash, UserRole role, string name)
    {
        ValidateUser(email, passwordHash);

        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        Name = name;
        IsActive = true;
    }

    public void UpdatePassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new RequiredValueException(nameof(PasswordHash));

        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    private void ValidateUser( string email, string passwordHash)
    {

        if (string.IsNullOrWhiteSpace(email))
            throw new EntityValidationException(nameof(User), "Email", Error.EmptyEmail.Message);

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new EntityValidationException(nameof(User), "Password", Error.PasswordInvalidFormat.Message);

        if (!IsValidEmail(email) )
            throw new EntityValidationException(nameof(User), "Email", Error.EmailInvalidFormat.Message);
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    public void UpdateProfile(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new RequiredValueException(nameof(Name));

        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidEntityOperationException(nameof(User), "Activate", "User is already active");

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidEntityOperationException(nameof(User), "Deactivate", "User is already inactive");

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
