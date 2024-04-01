using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class ValidatedRequiredProperty<TRequest>
	where TRequest : notnull
{
	private readonly CanFail<TRequest> _result;

	internal ValidatedRequiredProperty(CanFail<TRequest> result)
	{
		_result = result;
	}

	public CanFail<TRequest> Build()
	{
		return _result;
	}
}
