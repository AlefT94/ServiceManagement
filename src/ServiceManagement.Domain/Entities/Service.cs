namespace ServiceManagement.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public bool IsActive { get; set; }
    public List<ServiceImage> ServiceImages { get; set; } = new List<ServiceImage>();
}
