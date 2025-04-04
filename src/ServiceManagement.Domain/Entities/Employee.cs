namespace ServiceManagement.Domain.Entities;

public class Employee : ApplicationUser
{
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
