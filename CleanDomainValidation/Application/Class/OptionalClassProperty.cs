using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Class;

public sealed class OptionalClassProperty<TParameters, TProperty> : IValidatableProperty
	where TProperty : notnull
{
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal OptionalClassProperty(TParameters parameters)
	{
		Parameters = parameters;
	}

	public TProperty? Map(Func<TParameters, TProperty?> value)
	{
		return value.Invoke(Parameters);
	}
}
