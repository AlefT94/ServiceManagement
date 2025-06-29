namespace ServiceManagement.Domain.Entities;

public class Employee : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public int UserId { get; private set; }  
    public User User { get; private set; }
    public int CompanyId { get; private set; }
    public Company Company { get; private set; }
    public bool IsActive { get; private set; } = true;
}
