using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

public sealed class OptionalEnumProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	public bool IsRequired => false;

	public bool IsMissing { get; set; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal OptionalEnumProperty(TParameters parameters)
	{
		IsMissing = false;
		Parameters = parameters;
	}
}
