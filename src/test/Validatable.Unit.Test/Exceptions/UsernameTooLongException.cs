using Validatable.Exceptions;

namespace Validatable.Unit.Test.Exceptions;

public class UsernameTooLongException(string message) : ValidationException<ErrorCode?>(message)
{
    public override ErrorCode? Code { get; } = ErrorCode.UsernameTooLong;
}