using MediatR;

namespace ServiceManagement.Application.CompanyApplication.Commands.CreateCompany;

public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, string>
{
    public async Task<string> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        return "Funcionou";
    }
}
