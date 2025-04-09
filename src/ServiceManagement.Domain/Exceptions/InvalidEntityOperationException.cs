using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Exceptions;

public class InvalidEntityOperationException : DomainException
{
    public string EntityName { get; }
    public string Operation { get; }

    public InvalidEntityOperationException(string entityName, string operation, string message)
        : base(message)
    {
        EntityName = entityName;
        Operation = operation;
    }
}
