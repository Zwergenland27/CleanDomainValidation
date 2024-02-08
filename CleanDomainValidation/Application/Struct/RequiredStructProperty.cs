using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Struct;

public sealed class RequiredStructProperty<TParameters, TProperty> : IValidatableProperty
	where TProperty : struct
{
	public Error MissingError { get; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal RequiredStructProperty(TParameters parameters, Error missingError)
	{
		Parameters = parameters;
		MissingError = missingError;
	}
}
