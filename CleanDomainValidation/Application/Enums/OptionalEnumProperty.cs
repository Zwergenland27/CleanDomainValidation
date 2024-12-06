using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

/// <summary>
/// The property is an enum that cannot be null
/// </summary>
public sealed class OptionalEnumProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	internal TParameters Parameters { get; }
	
	internal TProperty? DefaultValue { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal OptionalEnumProperty(TParameters parameters, TProperty? defaultValue = null)
	{
		Parameters = parameters;
		DefaultValue = defaultValue;
	}
}
