using Validatable.Exceptions;

namespace Validatable.Unit.Test.Exceptions;

public class InvalidUsernameException(string message, ErrorCode? errorCode) : ValidationException<ErrorCode?>(message)
{
    public override ErrorCode? Code { get; } = errorCode;
}