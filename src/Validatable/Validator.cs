using Validatable.Exceptions;

namespace Validatable;

public abstract class Validator<TErrorCode>
{
    private IEnumerable<IValidatable<TErrorCode>> ValidatableProperties =>
        GetType().GetProperties()
            .Where(p => p.PropertyType.GetInterfaces().Contains(typeof(IValidatable<TErrorCode>)))
            .Select(p => (IValidatable<TErrorCode>)p.GetValue(this));

    protected bool StateIsValid(out ValidationException<TErrorCode>[] errors)
    {
        errors = ValidatableProperties
            .SelectMany(v => !v.IsValid(out var errs) ? errs : [])
            .ToArray();

        return errors.Length is 0;
    }
}