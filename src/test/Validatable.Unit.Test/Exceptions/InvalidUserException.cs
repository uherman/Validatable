using Validatable.Exceptions;

namespace Validatable.Unit.Test.Exceptions;

public class InvalidUserException(IReadOnlyCollection<ValidationException<ErrorCode?>> exceptions)
    : AggregateValidationException<ErrorCode?>(exceptions, [])
{
    public override ErrorCode? Code { get; } = null;
}