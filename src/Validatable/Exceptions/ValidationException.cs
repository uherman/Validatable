using System.Collections;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Validatable.Exceptions;

/// <summary>
/// Represents an exception that occurs during the validation process of a domain model.
/// </summary>
/// <param name="message">The message that describes the error.</param>
public abstract class ValidationException<TErrorCode>(string message) : Exception(message)
{
    public virtual TErrorCode Code { get; } = default;
    public virtual IReadOnlyCollection<ValidationException<TErrorCode>> Exceptions { get; } = [];

    public object ToJsonFormat() => new
    {
        Type = GetType().Name,
        ErrorCode = Code?.ToString(),
        ValidationErrors = Exceptions.Count is 0 ? null : Exceptions.Select(e => e.ToJsonFormat())
    };

    public string ToJson(JsonSerializerOptions options = null) => JsonSerializer.Serialize(ToJsonFormat(),
        options ?? new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
}

public abstract class AggregateValidationException<TErrorCode>(
    IReadOnlyCollection<ValidationException<TErrorCode>> exceptions,
    Dictionary<string, string> metadata = null)
    : ValidationException<TErrorCode>(CreateMessage(exceptions, metadata))
{
    public override IDictionary Data { get; } = metadata ?? [];

    private static string CreateMessage(IReadOnlyCollection<ValidationException<TErrorCode>> exceptions,
        Dictionary<string, string> metadata)
    {
        var builder = new StringBuilder("One or more validation errors occurred:\n");
        foreach (var exception in exceptions)
        {
            builder.AppendLine($"- {exception.GetType().Name}: {exception.Message}");
        }

        if (metadata is not null)
        {
            builder.AppendLine(BuildMetadata(metadata));
        }

        return builder.ToString();
    }

    private static string BuildMetadata(Dictionary<string, string> metadata)
    {
        var builder = new StringBuilder("\n");
        builder.AppendLine("Metadata:");

        builder.AppendLine(JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true }));

        return builder.ToString();
    }

    public override IReadOnlyCollection<ValidationException<TErrorCode>> Exceptions { get; } = exceptions;
}