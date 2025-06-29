namespace ServiceManagement.Domain.Entities;

public class ServiceImage : BaseEntity
{
    public string ImageUrl { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
}
