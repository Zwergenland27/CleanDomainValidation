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
	private Builder() { }

	/// <summary>
	/// Adds the unvalidated parameter object of type <typeparamref name="TParameters"/> to the builder
	/// </summary>
	/// <returns>Instance for a builder that maps parameters <typeparamref name="TParameters"/> to the request <typeparamref name="TRequest"/></returns>
	public static Builder<TParameters, TRequest> BindParameters<TParameters>(TParameters parameters)
		where TParameters : IParameters
	{
		return new Builder<TParameters, TRequest>(parameters);
	}
}

/// <summary>
/// Builder that maps parameters <typeparamref name="TParameters"/> to the request <typeparamref name="TRequest"/>
/// </summary>
public sealed class Builder<TParameters, TRequest>
	where TParameters : IParameters
	where TRequest : IRequest
{
	private TParameters _parameters;
	internal Builder(TParameters parameters)
	{
		_parameters = parameters;
	}

	/// <summary>
	/// Sets property <paramref name="propertyExpression"/> of the parameters object to <paramref name="value"/>
	/// </summary>
	/// <returns>The builder itself for chaining methods</returns>
	public Builder<TParameters, TRequest> MapParameter<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression, TProperty value)
	{
		var memberExpression = propertyExpression.Body as MemberExpression;
		if (memberExpression == null)
		{
			throw new ArgumentException($"Expression {propertyExpression} must be a member expression");
		}

		var property = memberExpression.Member as PropertyInfo;
		if (property == null)
		{
			throw new ArgumentException($"Expression {propertyExpression} must be a property expression");
		}

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
		return builder.Configure(new RequiredPropertyBuilder<TParameters, TRequest>(_parameters)).Build();
	}
}
