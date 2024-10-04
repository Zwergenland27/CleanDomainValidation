using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Structs;

public sealed class RequiredStructProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	internal Error MissingError { get; }
	internal TParameters Parameters { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal RequiredStructProperty(TParameters parameters, Error missingError)
	{
		Parameters = parameters;
		MissingError = missingError;
	}
}
