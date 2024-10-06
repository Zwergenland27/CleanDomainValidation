using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

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
