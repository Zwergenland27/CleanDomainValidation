using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class ValidatedProperty<TRequest>
{
	private readonly TRequest _request;

	private readonly CanFail _result;

	internal ValidatedProperty(TRequest request, CanFail result)
	{
		_request = request;
		_result = result;
	}

	public CanFail<TRequest> Build()
	{
		if (_result.HasFailed) return _result.GetFailureAs<TRequest>();
		return _request;
	}
}
