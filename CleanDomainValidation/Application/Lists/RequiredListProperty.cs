using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

/// <summary>
/// The property is a list that cannot be null
/// </summary>
public sealed class RequiredListProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : notnull
{
	internal Error MissingError { get; }
	internal TParameters Parameters { get; }
	internal NamingStack NamingStack { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal RequiredListProperty(TParameters parameters, Error missingError, NamingStack namingStack)
	{
		Parameters = parameters;
		MissingError = missingError;
		NamingStack = namingStack;
	}
}
