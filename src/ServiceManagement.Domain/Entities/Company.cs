using ServiceManagement.Domain.Enums;
using ServiceManagement.Domain.ValueObjects;
using System.Net;

namespace ServiceManagement.Domain.Entities;

public class Company : User
{
    public string CompanyName { get; private set; }
    public string CNPJ { get; private set; }
    public string PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public bool IsActive { get; private set; }
    public ICollection<Employee> Employees { get; set; }

    private Company() : base() { }

    public Company(string username, 
                   string email, 
                   string passwordHash,
                   string companyName, 
                   string cnpj,
                   string phoneNumber, 
                   Address address)
        : base(email, passwordHash, UserRole.Company)
    {
        CompanyName = companyName ?? throw new ArgumentNullException(nameof(companyName));
        CNPJ = cnpj ?? throw new ArgumentNullException(nameof(cnpj));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        IsActive = true;
    }

    public void UpdateProfile(string companyName, string tradeName, string phoneNumber, Address address)
    {
        CompanyName = companyName ?? throw new ArgumentNullException(nameof(companyName));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        Address = address ?? throw new ArgumentNullException(nameof(address));
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