using MediatR;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Enums;
using ServiceManagement.Domain.Errors;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.Domain.ValueObjects;

namespace ServiceManagement.Application.CompanyApplication.Commands.CreateCompany;

public class CreateCompanyHandler(IUserPasswordHasher passwordHasher, IUnitOfWork unitOfWork) : IRequestHandler<CreateCompanyCommand, Result>
{
    public async Task<Result> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
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
        catch(Exception ex)
        {
            return Result.Failure(new Error("CompanyCreationFailed", ex.Message));
        }
    }
}
