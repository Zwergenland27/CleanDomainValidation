using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

public sealed class OptionalClassProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : class
{
	internal TParameters Parameters { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal OptionalClassProperty(TParameters parameters)
	{
		Parameters = parameters;
	}
}
