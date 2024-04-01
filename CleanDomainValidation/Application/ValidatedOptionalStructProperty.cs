using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class ValidatedOptionalStructProperty<TRequest>
	where TRequest : struct
{
	private readonly CanFail<TRequest?> _result;

	internal ValidatedOptionalStructProperty(CanFail<TRequest?> result)
	{
		_result = result;
	}

	public CanFail<TRequest?> Build()
	{
		return _result;
	}
}
