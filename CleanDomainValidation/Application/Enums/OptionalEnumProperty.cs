using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

public sealed class OptionalEnumProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	internal TParameters Parameters { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal OptionalEnumProperty(TParameters parameters)
	{
		Parameters = parameters;
	}
}
