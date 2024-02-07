using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class Configured<TRequest>
{
	private readonly Func<TRequest> _creationMethod;

	private readonly CanFail _result;

	internal Configured(Func<TRequest> creationMethod, CanFail result)
	{
		_creationMethod = creationMethod;
		_result = result;
	}

	public CanFail<TRequest> Build()
	{
		if(_result.HasFailed) return _result.GetFailureAs<TRequest>();

		return _creationMethod.Invoke();
	}
}
