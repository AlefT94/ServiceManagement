using ServiceManagement.Domain.Exceptions;

namespace ServiceManagement.Domain.ValueObjects;

public class Address
{
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Complement { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }

    private Address() { }

    public Address(string street, string number, string city, string state, string zipCode,
                          string neighborhood = "", string complement = "")
    {
        ValidateAddress(street, number, city, state, zipCode);

        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    private void ValidateAddress(string street, string number, string city, string state, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new InvalidAddressException(nameof(Street), "Street cannot be empty");

        if (string.IsNullOrWhiteSpace(number))
            throw new InvalidAddressException(nameof(Number), "Number cannot be empty");

        if (string.IsNullOrWhiteSpace(city))
            throw new InvalidAddressException(nameof(City), "City cannot be empty");

        if (string.IsNullOrWhiteSpace(state))
            throw new InvalidAddressException(nameof(State), "State cannot be empty");

        if (string.IsNullOrWhiteSpace(zipCode))
            throw new InvalidAddressException(nameof(ZipCode), "ZipCode cannot be empty");
    }
}
