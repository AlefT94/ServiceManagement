namespace ServiceManagement.Domain.Errors;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "A null values isn't allowed");
    public static Error EmptyEmail = new("Error.EmptyEmail", "The e-mail must be filled");
    public static Error EmailInvalidFormat = new("Error.EmailInvalidFormat", "The e-mail format is invalid.");
    public static Error PasswordInvalidFormat = new("Error.PasswordInvalidFormat", "The password format is invalid.");
    public static Error UserCreatingError = new("Error.UserCreatingError", "Error while creating the user.");
}
