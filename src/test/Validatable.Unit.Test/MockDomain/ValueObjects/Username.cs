using Validatable.Exceptions;
using Validatable.Unit.Test.Exceptions;

namespace Validatable.Unit.Test.MockDomain.ValueObjects;

public readonly record struct Username : IValidatable<ErrorCode?>
{
    private const int MaxLength = 50;
    private readonly string _value;
    private readonly List<ValidationException<ErrorCode?>> _errors = [];

    public Username(string value)
    {
        if (value is not { Length: > 0 })
        {
            _errors.Add(new UsernameEmptyException());
            return;
        }

        if (value is { Length: > MaxLength })
        {
            _errors.Add(new UsernameTooLongException($"Name is too long. Max length is {MaxLength}"));
        }

        if (value.Contains("$∞§|[]]"))
        {
            _errors.Add(new InvalidUsernameException("Name contains invalid characters",
                ErrorCode.UsernameContainsInvalidCharacters));
        }

        _value = value;
    }

    public static implicit operator string(Username username) => username._value;
    public override string ToString() => _value;

    public bool IsValid(out IReadOnlyCollection<ValidationException<ErrorCode?>> errors)
    {
        errors = _errors;
        return errors.Count is 0;
    }
}