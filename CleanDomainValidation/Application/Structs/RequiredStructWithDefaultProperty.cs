using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Structs;

/// <summary>
/// The property is a struct with a default value if not set in request
/// </summary>
public class RequiredStructWithDefaultProperty<TParameters, TProperty> : ValidatableProperty
    where TParameters : notnull
    where TProperty : struct
{
    
    internal TParameters Parameters { get; }
    internal TProperty DefaultValue { get; }
    internal NameStack NameStack { get; }
    internal override CanFail ValidationResult { get; } = new();

    internal RequiredStructWithDefaultProperty(TParameters parameters, TProperty defaultValue, NameStack nameStack)
    {
        Parameters = parameters;
        DefaultValue = defaultValue;
        NameStack = nameStack;
    }
}