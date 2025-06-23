using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Enums;
using ServiceManagement.Domain.ValueObjects;

namespace ServiceManagement.API.Dto;

public record CreateCompanyDto(string companyName, 
    string phoneNumber, 
    string street, 
    string number, 
    string complement, 
    string neighborhood, 
    string state, 
    string city, 
    string zipCode,
    string email,
    string password,
    string userName)
{
    public Company ToEntity()
    {
        var address = new Address(
            street: street,
            number: number,
            city: city,
            state: state,
            zipCode: zipCode,
            neighborhood: neighborhood,
            complement: complement
        );

        var user = new User(
            email: email,
            passwordHash: password,
            role: UserRole.Company,
            name: userName
        );

        // Cria a Company
        var company = new Company(
            email: email,
            passwordHash: password,
            companyName: companyName,
            phoneNumber: phoneNumber,
            address: address,
            user: user
        );

        return company;
    }
}