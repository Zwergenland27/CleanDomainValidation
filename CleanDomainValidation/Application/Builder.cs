using CleanDomainValidation.Application.Class;
using CleanDomainValidation.Application.Struct;

namespace CleanDomainValidation.Application;

public class Builder<TParameters, TResult>
{
	private readonly TParameters _parameters;
	protected readonly List<IValidatableProperty> Properties = [];

	internal Builder(TParameters parameters)
	{
		_parameters = parameters;
	}

	public ClassProperty<TParameters, TProperty> ClassProperty<TProperty>(Func<TResult, TProperty?> property)
	where TProperty : notnull
	{
		var classProperty = new ClassProperty<TParameters, TProperty>(_parameters);
		Properties.Add(classProperty);
		return classProperty;
	}

	public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : struct
	{
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters);
		Properties.Add(structProperty);
		return structProperty;
	}

	public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Func<TResult, TProperty> property)
		where TProperty : struct
	{
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters);
		Properties.Add(structProperty);
		return structProperty;
	}
}
