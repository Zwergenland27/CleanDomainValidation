using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

/// <summary>
/// The property is a class that cannot be null
/// </summary>
public sealed class RequiredClassProperty<TParameters, TProperty> : ValidatableProperty
    where TParameters : notnull
    where TProperty : class
{
    internal Error MissingError { get; }
    internal TParameters Parameters { get; }
    internal NameStack NameStack { get; }
    internal override CanFail ValidationResult { get; } = new();

    internal RequiredClassProperty(TParameters parameters, Error missingError, NameStack nameStack)
    {
        Parameters = parameters;
        MissingError = missingError;
        NameStack = nameStack;
    }
}
