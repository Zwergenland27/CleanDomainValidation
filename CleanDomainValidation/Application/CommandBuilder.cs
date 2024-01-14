using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class CommandBuilder<TCommand>
	where TCommand : ICommand
{
	public static CommandBuilder<TParameter, TCommand> AddParameter<TParameter>(TParameter parameter)
		where TParameter : IParameter
	{
		return new CommandBuilder<TParameter, TCommand>(parameter);
	}
}

public sealed class CommandBuilder<TParameter, TCommand>
	where TParameter : IParameter
	where TCommand : ICommand
{
	private readonly TParameter _parameter;

	internal CommandBuilder(TParameter parameter)
	{
		_parameter = parameter;
	}

	public CanFail<TCommand> ValidateByUsing<TValidator>() where TValidator : CommandValidator<TParameter, TCommand>, new()
	{
		TValidator validator = new();
		validator.Configure(_parameter);
		return validator.Validate();
	}

	public CommandBuilder<TParameter, TUrlParameter, TCommand> AddUrlParameter<TUrlParameter>(TUrlParameter urlParameter)
		where TUrlParameter : IUrlParameter
	{
		return new CommandBuilder<TParameter, TUrlParameter, TCommand>(_parameter, urlParameter);
	}

}

public sealed class CommandBuilder<TParameter, TUrlParameter, TCommand>
	where TParameter : IParameter
	where TUrlParameter : IUrlParameter
	where TCommand : ICommand
{
	private readonly TParameter _parameter;
	private readonly TUrlParameter _urlParameter;

	internal CommandBuilder(TParameter parameter, TUrlParameter urlParameter)
	{
		_parameter = parameter;
		_urlParameter = urlParameter;
	}

	public CanFail<TCommand> ValidateByUsing<TValidator>() where TValidator : CommandValidator<TParameter, TUrlParameter, TCommand>, new()
	{
		TValidator validator = new();
		validator.Configure(_parameter, _urlParameter);
		return validator.Validate();
	}
}
