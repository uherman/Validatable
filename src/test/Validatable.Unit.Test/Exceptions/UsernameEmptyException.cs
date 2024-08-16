using Validatable.Exceptions;

namespace Validatable.Unit.Test.Exceptions;

public class UsernameEmptyException() : ValidationException<ErrorCode?>("Name is empty")
{
    public override ErrorCode? Code => ErrorCode.UsernameEmpty;
}