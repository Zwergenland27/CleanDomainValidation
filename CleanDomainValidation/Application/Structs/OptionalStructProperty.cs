using CleanDomainValidation.Domain;
using System.Linq.Expressions;

namespace CleanDomainValidation.Application.Structs;

public sealed class OptionalStructProperty<TParameters, TProperty> : IValidatableProperty
	where TProperty : struct
{
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal OptionalStructProperty(TParameters parameters)
	{
		Parameters = parameters;
	}
}
