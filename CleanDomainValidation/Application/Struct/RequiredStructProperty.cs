using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Struct;

public sealed class RequiredStructProperty<TParameters, TProperty> : IValidatableProperty
	where TProperty : struct
{
	public Error MissingError { get; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal RequiredStructProperty(TParameters parameters, Error missingError)
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

		return res.Value;
	}
}
