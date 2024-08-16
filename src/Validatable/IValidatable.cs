using Validatable.Exceptions;

namespace Validatable;

public interface IValidatable<TErrorCode>
{
    bool IsValid(out IReadOnlyCollection<ValidationException<TErrorCode>> errors);
}