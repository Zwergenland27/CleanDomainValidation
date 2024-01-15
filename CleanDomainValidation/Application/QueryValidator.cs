namespace CleanDomainValidation.Application;

/// <summary>
/// Validates <typeparamref name="TUrlParameter"/> to <typeparamref name="TQuery"/>
/// </summary>
public abstract class QueryValidator<TUrlParameter, TQuery> : Validator<TQuery>
	where TUrlParameter : IUrlParameter
	where TQuery : IQuery
{
	/// <summary>
	/// Configures how <paramref name="urlParameter"/> and <paramref name="urlParameter"/> should be validated to create <typeparamref name="TQuery"/> instance
	/// </summary>
	public abstract void Configure(TUrlParameter urlParameter);
}
