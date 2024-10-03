using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

public sealed class RequiredEnumProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	public Error MissingError { get; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal RequiredEnumProperty(TParameters parameters, Error missingError)
	{
		Parameters = parameters;
		MissingError = missingError;
	}
}