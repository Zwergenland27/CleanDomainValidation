using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

public sealed class OptionalClassProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : class
{
	public bool IsRequired => false;

	public bool IsMissing { get; set; }

	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal OptionalClassProperty(TParameters parameters)
	{
		IsMissing = false;
		Parameters = parameters;
	}
}
