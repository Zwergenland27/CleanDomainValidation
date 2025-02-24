using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Structs;

/// <summary>
/// The property is a struct that can be null
/// </summary>
public sealed class OptionalStructProperty<TParameters, TProperty> : ValidatableProperty
    where TParameters : notnull
    where TProperty : struct
{
    internal TParameters Parameters { get; }
    internal NamingStack NamingStack { get; }
    internal override CanFail ValidationResult { get; } = new();

    internal OptionalStructProperty(TParameters parameters, NamingStack namingStack)
    {
        Parameters = parameters;
        NamingStack = namingStack;
    }
}
