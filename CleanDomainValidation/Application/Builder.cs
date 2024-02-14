using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;

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

	public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : struct
	{
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters);
		Properties.Add(enumProperty);
		return enumProperty;
	}

	public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Func<TResult, TProperty> property)
		where TProperty : struct
	{
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters);
		Properties.Add(enumProperty);
		return enumProperty;
	}

	public ListProperty<TParameters, TProperty> ListProperty<TProperty>(Func<TResult, IEnumerable<TProperty>?> property)
		where TProperty : notnull
	{
		var classListProperty = new ListProperty<TParameters, TProperty>(_parameters);
		Properties.Add(classListProperty);
		return classListProperty;
	}
}
