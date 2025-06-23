using ServiceManagement.Domain.Interfaces;

namespace ServiceManegement.Infrastructure.Authentication;

public class UserPasswordHasher : IUserPasswordHasher
{
    public string Hash(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public bool Verify(string password, string hash)
    {
        return Hash(password) == hash;
    }
}
