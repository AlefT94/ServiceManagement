namespace ServiceManagement.Domain.Exceptions;

public class RequiredValueException : DomainException
{
    public string PropertyName { get; }

    public RequiredValueException(string propertyName)
        : base($"The {propertyName} cannot be null or empty.")
    {
        PropertyName = propertyName;
    }
}
