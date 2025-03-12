using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

/// <summary>
/// The property is an enum with a default value if not set in request
/// </summary>
public sealed class RequiredEnumWithDefaultProperty<TParameters, TProperty> : ValidatableProperty
    where TParameters : notnull
    where TProperty : struct
{
    internal TParameters Parameters { get; }
    internal TProperty DefaultValue { get; }
    internal NameStack NameStack { get; }
    internal override CanFail ValidationResult { get; } = new();

    internal RequiredEnumWithDefaultProperty(TParameters parameters, TProperty defaultValue, NameStack nameStack)
    {
        Parameters = parameters;
        DefaultValue = defaultValue;
        NameStack = nameStack;
    }
}