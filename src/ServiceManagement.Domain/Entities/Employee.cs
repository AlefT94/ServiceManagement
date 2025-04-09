using ServiceManagement.Domain.Enums;
using ServiceManagement.Domain.Exceptions;

namespace ServiceManagement.Domain.Entities;

public class Employee : User
{
    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Position { get; private set; }
    public int CompanyId { get; private set; }
    public bool IsActive { get; private set; }

    private Employee() : base() { }

    public Employee(string username, string email, string passwordHash,
                   string name, string phoneNumber, string position, int companyId)
        : base(email, passwordHash, UserRole.Employee)
    {
        ValidateEmployee(name, phoneNumber, position, companyId);

        Name = name;
        PhoneNumber = phoneNumber;
        Position = position;
        CompanyId = companyId;
        IsActive = true;
    }

    private void ValidateEmployee(string name, string phoneNumber, string position, int companyId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new RequiredValueException(nameof(Name));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new RequiredValueException(nameof(PhoneNumber));

        if (string.IsNullOrWhiteSpace(position))
            throw new RequiredValueException(nameof(Position));

        if (companyId == 0)
            throw new EntityValidationException(nameof(Employee), nameof(CompanyId), "Company ID cannot be empty");
    }

    public void UpdateProfile(string name, string phoneNumber, string position)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new RequiredValueException(nameof(Name));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new RequiredValueException(nameof(PhoneNumber));

        if (string.IsNullOrWhiteSpace(position))
            throw new RequiredValueException(nameof(Position));

        Name = name;
        PhoneNumber = phoneNumber;
        Position = position;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidEntityOperationException(nameof(Employee), "Activate", "Employee is already active");

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidEntityOperationException(nameof(Employee), "Deactivate", "Employee is already inactive");

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
