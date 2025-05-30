using ServiceManagement.Domain.Enums;
using ServiceManagement.Domain.Exceptions;
using ServiceManagement.Domain.ValueObjects;

namespace ServiceManagement.Domain.Entities;

public class Company : BaseEntity
{
    public string CompanyName { get; private set; }
    public string PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public bool IsActive { get; private set; }

    private Company() : base() { }

    public Company(string email, 
                   string passwordHash,
                   string companyName,
                   string phoneNumber, 
                   Address address)
    {
        ValidateCompany(companyName, phoneNumber, address);

        CompanyName = companyName;
        PhoneNumber = phoneNumber;
        Address = address;
        IsActive = true;
    }

    private void ValidateCompany(string companyName, string phoneNumber, Address address)
    {
        if (string.IsNullOrWhiteSpace(companyName))
            throw new RequiredValueException(nameof(CompanyName));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new RequiredValueException(nameof(PhoneNumber));

        if (address == null)
            throw new RequiredValueException(nameof(Address));
    }

    public void UpdateProfile(string companyName,  string phoneNumber, Address address)
    {
        if (string.IsNullOrWhiteSpace(companyName))
            throw new RequiredValueException(nameof(CompanyName));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new RequiredValueException(nameof(PhoneNumber));

        if (address == null)
            throw new RequiredValueException(nameof(Address));

        CompanyName = companyName;
        PhoneNumber = phoneNumber;
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidEntityOperationException(nameof(Company), "Activate", "Company is already active");

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidEntityOperationException(nameof(Company), "Deactivate", "Company is already inactive");

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}