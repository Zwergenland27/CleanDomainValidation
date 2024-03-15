using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

public sealed class RequiredListProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : notnull
{
	public bool IsRequired => true;
	public bool IsMissing { get; set; }
	public Error MissingError { get; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal RequiredListProperty(TParameters parameters, Error missingError)
	{
		IsMissing = false;
		Parameters = parameters;
		MissingError = missingError;
	}
}
