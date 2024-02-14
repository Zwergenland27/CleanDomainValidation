using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

public sealed class OptionalClassProperty<TParameters, TProperty> : IValidatableProperty
	where TProperty : notnull
{
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal OptionalClassProperty(TParameters parameters)
	{
		Parameters = parameters;
	}
}
