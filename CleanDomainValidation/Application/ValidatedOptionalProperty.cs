using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class ValidatedOptionalProperty<TRequest>
	where TRequest : notnull
{
	private readonly CanFail<TRequest?> _result;

	internal ValidatedOptionalProperty(CanFail<TRequest?> result)
	{
		_result = result;
	}

	public CanFail<TRequest?> Build()
	{
		return _result;
	}
}
