using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public class Build<TRequest>
{
	private Build() { }

    public static Build<TParameters, TRequest> ByAdding<TParameters>(TParameters parameters)
	{
		return new Build<TParameters, TRequest>(parameters);
	}
}

public class Build<TParameters, TRequest>
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
		return builder.Configure(new RequestBuilder<TParameters, TRequest>(_parameters)).Build();
	}
}
