namespace ServiceManagement.Domain.Exceptions;

public class EntityValidationException : DomainException
{
    public string EntityName { get; }
    public string PropertyName { get; }

    public EntityValidationException(string entityName, string propertyName, string message)
        : base(message)
    {
        EntityName = entityName;
        PropertyName = propertyName;
    }
}
