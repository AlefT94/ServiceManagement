namespace ServiceManagement.Domain.Entities;

public class Company : ApplicationUser
{
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
