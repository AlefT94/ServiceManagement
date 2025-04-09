using ServiceManagement.Domain.Enums;

namespace ServiceManagement.Domain.Entities;

public class Employee : User
{
    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public int CompanyId { get; private set; }
    public bool IsActive { get; private set; }

    private Employee() : base() { }

    public Employee(string username, string email, string passwordHash,
                   string name, string phoneNumber, int companyId)
        : base(email, passwordHash, UserRole.Employee)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        CompanyId = companyId;
        IsActive = true;
    }

    public void UpdateProfile(string name, string phoneNumber, string position)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
