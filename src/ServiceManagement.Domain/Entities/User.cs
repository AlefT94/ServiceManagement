using ServiceManagement.Domain.Enums;
using ServiceManagement.Domain.Exceptions;

namespace ServiceManagement.Domain.Entities;

public abstract class User : BaseEntity
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public UserRole Role { get; private set; }
    public Company Company { get; private set; }
    public bool IsActive { get; set; }

    protected User(){ }

    protected User(string email, string passwordHash, UserRole role, string name, string phoneNumber, Company company)
    {
        ValidateUser(email, passwordHash);

        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        Company = company;
        Name = name;
        PhoneNumber = phoneNumber;
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
            throw new RequiredValueException(nameof(Email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new RequiredValueException(nameof(PasswordHash));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new RequiredValueException(nameof(Name));

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
    public void UpdateProfile(string name, string phoneNumber, string position)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new RequiredValueException(nameof(Name));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new RequiredValueException(nameof(PhoneNumber));

        Name = name;
        PhoneNumber = phoneNumber;
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
