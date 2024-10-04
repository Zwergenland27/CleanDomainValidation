using CleanDomainValidation.Domain;
using System.Linq.Expressions;

namespace CleanDomainValidation.Application.Structs;

public sealed class OptionalStructProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	internal TParameters Parameters { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal OptionalStructProperty(TParameters parameters)
	{
		Parameters = parameters;
	}
}
