using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Interfaces;

public interface IUserPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}
