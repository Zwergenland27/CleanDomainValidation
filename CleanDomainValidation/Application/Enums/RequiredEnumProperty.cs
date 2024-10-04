using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

public sealed class RequiredEnumProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	internal Error MissingError { get; }
	internal TParameters Parameters { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal RequiredEnumProperty(TParameters parameters, Error missingError)
	{
		Parameters = parameters;
		MissingError = missingError;
	}
}