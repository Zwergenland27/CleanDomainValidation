using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Structs;

/// <summary>
/// The property is a struct that cannot be null
/// </summary>
public sealed class RequiredStructProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	internal Error MissingError { get; }
	internal TParameters Parameters { get; }
	internal NameStack NameStack { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal RequiredStructProperty(TParameters parameters, Error missingError, NameStack nameStack)
	{
		Parameters = parameters;
		MissingError = missingError;
		NameStack = nameStack;
	}
}
