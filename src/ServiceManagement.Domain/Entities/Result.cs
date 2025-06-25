using ServiceManagement.Domain.Errors;
using System.Diagnostics.CodeAnalysis;

namespace ServiceManagement.Domain.Entities;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException();

            case false when error == Error.None:
                throw new InvalidOperationException();

            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    public static Result<T> Success(T value) => new(value, true, Error.None);
    public static Result<T> Failure(Error error) => new(default, false, error);

    public static Result<T> Create(T? value) =>
        value is not null ? Success(value) : Failure(Error.NullValue);

    protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
        => _value = value;

    [NotNull]
    public T Value => IsSuccess
    ? _value! ?? throw new InvalidOperationException("Value is null, but Result is successful.")
    : _value!;

    public static implicit operator Result<T>(T? value) => Create(value);
}
