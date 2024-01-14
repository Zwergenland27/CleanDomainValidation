namespace CleanDomainValidation.Application;

public abstract class QueryValidator<TUrlParameter, TQuery> : Validator<TQuery>
	where TUrlParameter : IUrlParameter
	where TQuery : IQuery
{
	public abstract void Configure(TUrlParameter parameter);
}
