using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Exceptions;

public class InvalidAddressException : DomainException
{
    public string PropertyName { get; }

    public InvalidAddressException(string propertyName, string message)
        : base(message)
    {
        PropertyName = propertyName;
    }
}
