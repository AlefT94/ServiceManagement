using MediatR;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Errors;
using ServiceManagement.Domain.Interfaces;

namespace ServiceManagement.Application.LoginApplication.Queries;

public class LoginHandler(IUnitOfWork unitOfWork, IUserPasswordHasher passwordHasher, ITokenGenerator tokenGenerator) : IRequestHandler<LoginQuery, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.User.GetByEmailAsync(request.email, cancellationToken);
        if (user == null)
        {
            return Result<LoginResponse>.Failure(Error.InvalidCredentials);
        }

        if (!passwordHasher.Verify(request.password, user.PasswordHash))
        {
            return Result<LoginResponse>.Failure(Error.InvalidCredentials);
        }

        var token = tokenGenerator.GenerateToken(user);

        return Result<LoginResponse>.Success(new LoginResponse(
            token,
            new LoginUserResponse(user.Email, user.Name, user.Role.ToString())
        ));
    }
}
