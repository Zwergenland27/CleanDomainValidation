namespace CleanDomainValidation.Application;

/// <summary>
/// Validates <typeparamref name="TParameter"/> to <typeparamref name="TCommand"/>
/// </summary>
public abstract class CommandValidator<TParameter, TCommand> : Validator<TCommand>
	where TParameter : IParameter
	where TCommand : ICommand
{
	/// <summary>
	/// Configures how <paramref name="parameter"/> should be validated to create <typeparamref name="TCommand"/> instance
	/// </summary>
	public abstract void Configure(TParameter parameter);
}

/// <summary>
/// Validates <typeparamref name="TParameter"/> and <typeparamref name="TUrlParameter"/> to <typeparamref name="TCommand"/>
/// </summary>
public abstract class CommandValidator<TParameter, TUrlParameter, TCommand> : Validator<TCommand>
	where TParameter : IParameter
	where TUrlParameter : IUrlParameter
	where TCommand : ICommand
{
	/// <summary>
	/// Configures how <paramref name="parameter"/> and <paramref name="urlParameter"/> should be validated to create <typeparamref name="TCommand"/> instance
	/// </summary>
	public abstract void Configure(TParameter parameter, TUrlParameter urlParameter);
}
