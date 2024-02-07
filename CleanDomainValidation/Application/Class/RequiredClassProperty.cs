using CleanDomainValidation.Domain;
using System.Linq.Expressions;

namespace CleanDomainValidation.Application.Class;

public sealed class RequiredClassProperty<TParameters, TProperty> : IValidatableProperty
	where TProperty : notnull
{
	public Error MissingError { get; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal RequiredClassProperty(TParameters parameters, Error missingError)
	{
		Parameters = parameters;
		MissingError = missingError;
	}

	public TProperty Map(Func<TParameters, TProperty?> value)
	{
		var res = value.Invoke(Parameters);
		if (res is null)
		{
			ValidationResult.Failed(MissingError);
			return default!;
		}

		return res;
	}
}
