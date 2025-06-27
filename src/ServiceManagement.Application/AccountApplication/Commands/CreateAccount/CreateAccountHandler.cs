using Microsoft.Extensions.Caching.Memory;
using ServiceManagement.Domain.Enums;
using ServiceManagement.Domain.Errors;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.Domain.ValueObjects;

namespace ServiceManagement.Application.AccountApplication.Commands.CreateAccount;

public class CreateAccountHandler(IUserPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IRequestHandler<CreateAccountCommand, Result>
{
    public async Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        string codeKey = $"VerificationCode_{request.email}";
        string attemptsKey = $"VerificationAttempts_{request.email}";
        string resendKey = $"VerificationResend_{request.email}";
        string blacklistKey = $"VerificationBlacklist_{request.email}";

        // 1.Check the blacklist
        if (memoryCache.TryGetValue(blacklistKey, out _))
        {
            // User blocked
            return Result.Failure(Error.AccountBlocked);
        }

        // 2. Code validation
        if (memoryCache.TryGetValue(codeKey, out string savedCode))
        {
            if (request.validationCode == savedCode)
            {
                memoryCache.Remove(attemptsKey);
                memoryCache.Remove(codeKey);
            }
            else
            {
                // Increment attempts
                int attempts = memoryCache.GetOrCreate(attemptsKey, entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                    return 0;
                });

                attempts++;
                memoryCache.Set(attemptsKey, attempts, TimeSpan.FromMinutes(5));

                if (attempts >= 3)
                {
                    memoryCache.Remove(codeKey);
                    memoryCache.Remove(attemptsKey);
                    return Result.Failure(Error.AtemptsExceded);
                }
                else
                {
                    return Result.Failure(Error.IncorrectCode);
                }
            }
        }
        else
        {
            return Result.Failure(Error.IncorrectCode);
        }

        try
        {
            var hashedPassword = passwordHasher.Hash(request.password);

            var newUser = new User(
                request.email,
                hashedPassword,
                UserRole.Company,
                request.userName);

            Address address = new Address(
                request.street,
                request.number,
                request.complement,
                request.neighborhood,
                request.state,
                request.city,
                request.zipCode);

            var newCompany = new Company(
                request.companyName,
                request.phoneNumber,
                address,
                newUser);

            await unitOfWork.Company.AddAsync(newCompany, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("AccountCreationFailed", ex.Message));
        }
    }
}

