using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Class;

public sealed class RequiredClassProperty<TParameters, TProperty> : IValidatableProperty
	where TProperty : notnull
{
	public Error MissingError { get; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal RequiredClassProperty(TParameters parameters, Error missingError)
	{
		Parameters = parameters;
		MissingError = missingError;
	}
}
