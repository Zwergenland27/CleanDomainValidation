using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class RequestBuilder<TParameter, TRequest> : Builder<TParameter, TRequest>
	where TParameter : IParameters
	where TRequest : IRequest
{
	internal RequestBuilder(TParameter parameters) : base(parameters) { }

	public ValidatedRequest<TRequest> Build(Func<TRequest> creationMethod)
	{
		CanFail result = new();

		Properties.ForEach(property =>
		{
			result.InheritFailure(property.ValidationResult);
		});

		if (result.HasFailed)
		{
			return new ValidatedRequest<TRequest>(default!, result);
		}

		return new ValidatedRequest<TRequest>(creationMethod.Invoke(), result);
	}
}
