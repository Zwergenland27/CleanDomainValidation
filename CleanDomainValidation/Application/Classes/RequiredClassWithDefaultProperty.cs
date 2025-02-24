using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

/// <summary>
/// The property is a class with a default value if not set in request
/// </summary>
public class RequiredClassWithDefaultProperty<TParameters, TProperty> : ValidatableProperty
    where TParameters : notnull
    where TProperty : class
{
    internal TParameters Parameters { get; }
    internal TProperty DefaultValue { get; }
    internal NamingStack NamingStack { get; }
    internal override CanFail ValidationResult { get; } = new();

    internal RequiredClassWithDefaultProperty(TParameters parameters, TProperty defaultValue, NamingStack namingStack)
    {
        Parameters = parameters;
        NamingStack = namingStack;
        DefaultValue = defaultValue;
    }
}