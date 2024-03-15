using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

public sealed class OptionalListProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : notnull
{
	public bool IsRequired => false;

	public bool IsMissing { get; set; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal OptionalListProperty(TParameters parameters)
	{
		IsMissing = false;
		Parameters = parameters;
	}
}
