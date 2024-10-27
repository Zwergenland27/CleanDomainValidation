using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

/// <summary>
/// The property is a list that can be null
/// </summary>
public sealed class OptionalListProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : notnull
{
	internal TParameters Parameters { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal OptionalListProperty(TParameters parameters)
	{
		Parameters = parameters;
	}
}
