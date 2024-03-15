using CleanDomainValidation.Domain;
using System.Linq.Expressions;

namespace CleanDomainValidation.Application.Structs;

public sealed class OptionalStructProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	public bool IsRequired => false;

	public bool IsMissing { get; set; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal OptionalStructProperty(TParameters parameters)
	{
		IsMissing = false;
		Parameters = parameters;
	}
}
