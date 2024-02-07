using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class RequestBuilder<TParameter, TRequest> : Builder<TParameter, TRequest>
{
	internal RequestBuilder(TParameter parameters) : base(parameters) { }

	public Configured<TRequest> Build(Func<TRequest> creationMethod)
	{
		CanFail result = new();

		Properties.ForEach(property =>
		{
			result.InheritFailure(property.ValidationResult);
		});

		return new Configured<TRequest>(creationMethod, result);
	}
}
