using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

public sealed class RequiredListProperty<TParameters, TProperty> : IValidatableProperty
{
	public Error MissingError { get; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal RequiredListProperty(TParameters parameters, Error missingError)
	{
		Parameters = parameters;
		MissingError = missingError;
	}
}
