using MediatR;

namespace ServiceManagement.Application.Company.Commands.CreateCompany;

public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, string>
{
    public async Task<string> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        return "Funcionou";
    }
}
