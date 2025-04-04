using Microsoft.AspNetCore.Identity;
using ServiceManagement.Domain.Enums;

namespace ServiceManagement.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public UserType UserType { get; set; }
    }
}
