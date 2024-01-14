using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class QueryBuilder<TQuery>
	where TQuery : IQuery
{
	public static QueryBuilder<TUrlParameter, TQuery> AddUrlParameter<TUrlParameter>(TUrlParameter urlParameter)
		where TUrlParameter : IUrlParameter
	{
		return new QueryBuilder<TUrlParameter, TQuery>(urlParameter);
	}
}

public sealed class QueryBuilder<TUrlParameter, TQuery>
	where TUrlParameter : IUrlParameter
	where TQuery : IQuery
{
	private readonly TUrlParameter _urlParameter;

	internal QueryBuilder(TUrlParameter urlParameter)
	{
		_urlParameter = urlParameter;
	}

	public CanFail<TQuery> ValidateByUsing<TValidator>()
		where TValidator : QueryValidator<TUrlParameter, TQuery>, new()
	{
		TValidator validator = new();
		validator.Configure(_urlParameter);
		return validator.Validate();
	}
}
