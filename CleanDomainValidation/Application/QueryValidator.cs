namespace CleanDomainValidation.Application;

public abstract class QueryValidator<TUrlParameters, TCommand> : Validator<TCommand>
	where TUrlParameters : IUrlParameter
{
	public abstract void Configure(TUrlParameters parameters);
}
