using MediatR;
using ServiceManagement.Domain.Entities;

namespace ServiceManagement.Application.Logins.Queries;

public record LoginUserResponse(string email, string name, string role);
public record LoginResponse(string token, LoginUserResponse user);
public record LoginQuery(string email, string password) : IRequest<Result<LoginResponse>>;

