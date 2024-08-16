using Validatable.Unit.Test.Exceptions;
using Validatable.Unit.Test.MockDomain;
using Validatable.Unit.Test.MockDomain.ValueObjects;
using Xunit.Abstractions;

namespace Validatable.Unit.Test;

public class UserTests(ITestOutputHelper testOutputHelper)
{
    [Theory]
    [InlineData(null)]
    public void Save_WhenUsernameIsInvalid_ShouldThrowInvalidUserException(string input)
    {
        // Arrange
        var username = new Username(input);
        var user = new User(username);

        // Act & Assert
        var exception = Assert.Throws<InvalidUserException>(() => user.Save());
        Assert.Contains(exception.Exceptions, e => e is UsernameEmptyException { Code: ErrorCode.UsernameEmpty });

        testOutputHelper.WriteLine(exception.ToJson());
    }
}