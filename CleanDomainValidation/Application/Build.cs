using CleanDomainValidation.Domain;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanDomainValidation.Application;

public class Build<TRequest>
	where TRequest : IRequest
{
	private Build() { }

	public static Build<TParameters, TRequest> Bind<TParameters>(TParameters parameters)
		where TParameters : IParameters
	{
		return new Build<TParameters, TRequest>(parameters);
	}
}

public class Build<TParameters, TRequest>
	where TParameters : IParameters
	where TRequest : IRequest
{
	private TParameters _parameters;
	internal Build(TParameters parameters)
	{
		_parameters = parameters;
	}

	public Build<TParameters, TRequest> Map<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression, TProperty value)
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

	public CanFail<TRequest> Using<TRequestBuilder>()
		where TRequestBuilder : IRequestBuilder<TParameters, TRequest>, new()
	{
		TRequestBuilder builder = new();
		return builder.Configure(new RequiredPropertyBuilder<TParameters, TRequest>(_parameters)).Build();
	}
}
