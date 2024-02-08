using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

public sealed class OptionalEnumProperty<TParameters, TProperty> : IValidatableProperty
	where TProperty : struct
{
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal OptionalEnumProperty(TParameters parameters)
	{
		Parameters = parameters;
	}
}
