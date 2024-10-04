using CleanDomainValidation.Domain;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanDomainValidation.Application;

public class Builder<TRequest>
	where TRequest : IRequest
{
	private Builder() { }

	public static Builder<TParameters, TRequest> BindParameters<TParameters>(TParameters parameters)
		where TParameters : IParameters
	{
		return new Builder<TParameters, TRequest>(parameters);
	}
}

public class Builder<TParameters, TRequest>
	where TParameters : IParameters
	where TRequest : IRequest
{
	private readonly TParameters _parameters;
	internal Builder(TParameters parameters)
	{
		_parameters = parameters;
	}

	public Builder<TParameters, TRequest> MapParameter<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression, TProperty value)
	{
		var memberExpression = propertyExpression.Body as MemberExpression ?? throw new ArgumentException($"Expression {propertyExpression} must be a member expression");
        var property = memberExpression.Member as PropertyInfo ?? throw new ArgumentException($"Expression {propertyExpression} must be a property expression");
        property.SetValue(_parameters, value);
		return this;
	}

	public CanFail<TRequest> BuildUsing<TRequestBuilder>()
		where TRequestBuilder : IRequestBuilder<TParameters, TRequest>, new()
	{
		TRequestBuilder builder = new();
		return builder.Configure(new RequiredPropertyBuilder<TParameters, TRequest>(_parameters)).Build();
	}
}
