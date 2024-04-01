using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

public sealed class RequiredEnumProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	public bool IsRequired => true;
	public bool IsMissing { get; set; }
	public Error MissingError { get; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal RequiredEnumProperty(TParameters parameters, Error missingError)
	{
		IsMissing = false;
		Parameters = parameters;
		MissingError = missingError;
	}
}