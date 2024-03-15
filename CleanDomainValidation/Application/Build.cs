using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public class Build<TRequest>
	where TRequest : IRequest
{
	private Build() { }

    public static Build<TParameters, TRequest> ByAdding<TParameters>(TParameters parameters)
		where TParameters : IParameters
	{
		return new Build<TParameters, TRequest>(parameters);
	}
}

public class Build<TParameters, TRequest>
	where TParameters : IParameters
	where TRequest : IRequest
{
	private TParameters _parameters;
	internal Build(TParameters parameters)
	{
		_parameters = parameters;
	}
	public CanFail<TRequest> Using<TRequestBuilder>()
		where TRequestBuilder : IRequestBuilder<TParameters, TRequest>, new()
	{
		TRequestBuilder builder = new();
		return builder.Configure(new RequiredPropertyBuilder<TParameters, TRequest>(_parameters)).Build();
	}
}
