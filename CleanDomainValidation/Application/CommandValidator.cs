namespace CleanDomainValidation.Application;

public abstract class CommandValidator<TParameters, TCommand> : Validator<TCommand>
	where TParameters : IParameter
	where TCommand : ICommand
{
	public abstract void Configure(TParameters parameters);
}

public abstract class CommandValidator<TParameters, TUrlParameters, TCommand> : Validator<TCommand>
	where TParameters : IParameter
	where TUrlParameters : IUrlParameter
	where TCommand : ICommand
{
	public abstract void Configure(TParameters parameters, TUrlParameters urlParameters);
}
