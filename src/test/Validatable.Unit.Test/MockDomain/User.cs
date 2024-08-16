using Validatable.Unit.Test.Exceptions;
using Validatable.Unit.Test.MockDomain.ValueObjects;

namespace Validatable.Unit.Test.MockDomain;

public class User(Username name) : Validator<ErrorCode?>
{
    public Username Name { get; } = name;

    public void Save()
    {
        EnsureValidState();
        // Save user
    }

    private void EnsureValidState()
    {
        if (!StateIsValid(out var errors))
        {
            throw new InvalidUserException(errors);
        }
    }
}