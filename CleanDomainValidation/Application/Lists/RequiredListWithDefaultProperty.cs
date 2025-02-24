using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

/// <summary>
/// The property is a list with a default list if not set in request
/// </summary>
public sealed class RequiredListWithDefaultProperty<TParameters, TProperty> : ValidatableProperty
    where TParameters : notnull
    where TProperty : notnull
{
    internal TParameters Parameters { get; }
    internal IEnumerable<TProperty> DefaultList { get; }
    internal NamingStack NamingStack { get; }
    internal override CanFail ValidationResult { get; } = new();

    internal RequiredListWithDefaultProperty(TParameters parameters, IEnumerable<TProperty> defaultList, NamingStack namingStack)
    {
        Parameters = parameters;
        DefaultList = defaultList;
        NamingStack = namingStack;
    }
}