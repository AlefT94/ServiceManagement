using ServiceManagement.Domain.Enums;
using ServiceManagement.Domain.Exceptions;

namespace ServiceManagement.Domain.Entities;

public abstract class User : BaseEntity
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }

    protected User(){ }

    protected User(string email, string passwordHash, UserRole role)
    {
        ValidateUser(email, passwordHash);

        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
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
            throw new RequiredValueException(nameof(Email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new RequiredValueException(nameof(PasswordHash));

        if (!IsValidEmail(email))
            throw new EntityValidationException(nameof(User), nameof(Email), "Invalid email format");
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
}
