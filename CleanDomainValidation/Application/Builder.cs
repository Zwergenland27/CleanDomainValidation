using CleanDomainValidation.Domain;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanDomainValidation.Application;

/// <summary>
/// Builder for creating a validated request of type <typeparamref name="TRequest"/>
/// </summary>
public class Builder<TRequest>
	where TRequest : IRequest
{
	private readonly string _prefix;

	private Builder(string prefix)
	{
		_prefix = prefix;
	}

	/// <summary>
	/// Sets the name that will prefix the automatically generated error messages
	/// </summary>
	/// <param name="name">Unique name of the endpoint</param>
	/// <example>User.ChangeName</example>
	public static Builder<TRequest> WithName(string name)
	{
		return new Builder<TRequest>(name);
	}
	
	/// <summary>
	/// Adds the unvalidated parameter object of type <typeparamref name="TParameters"/> to the builder
	/// </summary>
	/// <returns>Instance for a builder that maps parameters <typeparamref name="TParameters"/> to the request <typeparamref name="TRequest"/></returns>
	public Builder<TParameters, TRequest> BindParameters<TParameters>(TParameters parameters)
		where TParameters : IParameters
	{
		return new Builder<TParameters, TRequest>(parameters, _prefix);
	}
}

/// <summary>
/// Builder that maps parameters <typeparamref name="TParameters"/> to the request <typeparamref name="TRequest"/>
/// </summary>
public sealed class Builder<TParameters, TRequest>
	where TParameters : IParameters
	where TRequest : IRequest
{
	private readonly TParameters _parameters;
	private readonly string _prefix;
	internal Builder(TParameters parameters, string prefix)
	{
		_parameters = parameters;
		_prefix = prefix;
	}

	/// <summary>
	/// Sets property <paramref name="propertyExpression"/> of the parameters object to <paramref name="value"/>
	/// </summary>
	/// <returns>The builder itself for chaining methods</returns>
	public Builder<TParameters, TRequest> MapParameter<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression, TProperty value)
	{
		var memberExpression = propertyExpression.Body as MemberExpression ?? throw new ArgumentException($"Expression {propertyExpression} must be a member expression");
        var property = memberExpression.Member as PropertyInfo ?? throw new ArgumentException($"Expression {propertyExpression} must be a property expression");
        property.SetValue(_parameters, value);
		return this;
	}

	/// <summary>
	/// Builds the request object using a request builder of type <typeparamref name="TRequestBuilder"/>
	/// </summary>
	/// <typeparam name="TRequestBuilder"></typeparam>
	/// <returns></returns>
	public CanFail<TRequest> BuildUsing<TRequestBuilder>()
		where TRequestBuilder : IRequestBuilder<TParameters, TRequest>, new()
	{
		TRequestBuilder builder = new();
		var nameStack = new NameStack(_prefix);
		return builder.Configure(new RequiredPropertyBuilder<TParameters, TRequest>(_parameters, nameStack)).Build();
	}
}
