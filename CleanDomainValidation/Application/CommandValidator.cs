namespace CleanDomainValidation.Application;

public abstract class CommandValidator<TParameter, TCommand> : Validator<TCommand>
	where TParameter : IParameter
	where TCommand : ICommand
{
	public abstract void Configure(TParameter parameter);
}

public abstract class CommandValidator<TParameter, TUrlParameter, TCommand> : Validator<TCommand>
	where TParameter : IParameter
	where TUrlParameter : IUrlParameter
	where TCommand : ICommand
{
	public abstract void Configure(TParameter parameter, TUrlParameter urlParameter);
}
