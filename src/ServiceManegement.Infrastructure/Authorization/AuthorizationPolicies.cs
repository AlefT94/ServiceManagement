namespace ServiceManegement.Infrastructure.Authorization;

public static class AuthorizationPolicies
{
    public const string AdminOnly = "AdminOnly";
    public const string EmployeeAccess = "EmployeeAccess";
    public const string CompanyAccess = "CompanyAccess";
    public const string AllUsers = "AllUsers";
}