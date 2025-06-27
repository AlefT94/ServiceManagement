using Microsoft.Extensions.Caching.Memory;

namespace ServiceManagement.Application.AccountApplication.Commands.GenerateAccountConfirmationCode;

public class GenerateAccountConfirmationCodeHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IRequestHandler<GenerateAccountConfirmationCodeCommand, Result>
{
    public async Task<Result> Handle(GenerateAccountConfirmationCodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(request.email);
            if (addr.Address != request.email)
            {
                return Result.Failure(Error.EmailInvalidFormat);
            }
        }
        catch
        {
            return Result.Failure(Error.EmailInvalidFormat);
        }

        string resendKey = $"VerificationResend_{request.email}";
        string codeKey = $"VerificationCode_{request.email}";
        string blacklistKey = $"VerificationBlacklist_{request.email}";

        // 1.Check the blacklist
        if (memoryCache.TryGetValue(blacklistKey, out _))
        {
            // User blocked
            return Result.Failure(Error.AccountBlocked);
        }

        int resendCount = memoryCache.GetOrCreate(resendKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return 0;
        });

        resendCount++;
        memoryCache.Set(resendKey, resendCount, TimeSpan.FromMinutes(30));

        if (resendCount > 3)
        {
            // Add to the blacklist
            memoryCache.Set(blacklistKey, true, TimeSpan.FromMinutes(10));
            return Result.Failure(Error.AccountBlocked);
        }

        var userExists = await unitOfWork.User.ExistsAsync(request.email, cancellationToken);

        if (userExists)
        {
            return Result.Failure(Error.UserAlreadyExists);
        }

        string confirmationCode = new Random().Next(0, 99999).ToString();
        confirmationCode = confirmationCode.PadLeft(5, '0');

        memoryCache.Set(codeKey, confirmationCode, TimeSpan.FromMinutes(3));

        //TODO: Send confirmation code via email

        return Result.Success();
    }
}

