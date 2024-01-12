using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class CommandBuilder<TCommand>
	where TCommand : ICommand
{
	public static CommandBuilder<TParameters, TCommand> AddParameters<TParameters>(TParameters parameters)
		where TParameters : IParameter
	{
		return new CommandBuilder<TParameters, TCommand>(parameters);
	}
}

public sealed class CommandBuilder<TParameters, TCommand>
	where TParameters : IParameter
	where TCommand : ICommand
{
	private readonly TParameters _parameters;

	internal CommandBuilder(TParameters parameters)
	{
		_parameters = parameters;
	}

	public CanFail<TCommand> ValidateByUsing<TValidator>() where TValidator : CommandValidator<TParameters, TCommand>, new()
	{
		TValidator validator = new();
		validator.Configure(_parameters);
		return validator.Validate();
	}

	public CommandBuilder<TParameters, TUrlParameters, TCommand> AddUrlParameters<TUrlParameters>(TUrlParameters urlParameters) where TUrlParameters : IUrlParameter
	{
		return new CommandBuilder<TParameters, TUrlParameters, TCommand>(_parameters, urlParameters);
	}

}

public sealed class CommandBuilder<TParameters, TUrlParameters, TCommand>
	where TParameters : IParameter
	where TUrlParameters : IUrlParameter
	where TCommand : ICommand
{
	private readonly TParameters _parameters;
	private readonly TUrlParameters _urlParameters;

	internal CommandBuilder(TParameters parameters, TUrlParameters urlParameters)
	{
		_parameters = parameters;
		_urlParameters = urlParameters;
	}

	public CanFail<TCommand> ValidateByUsing<TValidator>() where TValidator : CommandValidator<TParameters, TUrlParameters, TCommand>, new()
	{
		TValidator validator = new();
		validator.Configure(_parameters, _urlParameters);
		return validator.Validate();
	}
}
