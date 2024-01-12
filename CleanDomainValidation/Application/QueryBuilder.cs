using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class QueryBuilder<TQuery>
	where TQuery : IQuery
{
	public static QueryBuilder<TUrlParameter, TQuery> AddUrlParameters<TUrlParameter>(TUrlParameter urlParameter)
		where TUrlParameter : IUrlParameter
	{
		return new QueryBuilder<TUrlParameter, TQuery>(urlParameter);
	}
}

public sealed class QueryBuilder<TUrlParameters, TCommand>
	where TUrlParameters : IUrlParameter
	where TCommand : ICommand
{
	private readonly TUrlParameters _urlParameters;

	internal QueryBuilder(TUrlParameters urlParameters)
	{
		_urlParameters = urlParameters;
	}

	public CanFail<TCommand> ValidateByUsing<TValidator>() where TValidator : QueryValidator<TUrlParameters, TCommand>, new()
	{
		TValidator validator = new();
		validator.Configure(_urlParameters);
		return validator.Validate();
	}
}
