using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;

namespace CleanDomainValidation.Application;
public abstract class PropertyBuilder<TParameters, TResult>
	where TParameters : notnull
	where TResult : notnull
{
	private readonly TParameters _parameters;
	private readonly List<ValidatableProperty> _properties = [];
	protected IReadOnlyList<ValidatableProperty> Properties => _properties.AsReadOnly();

	internal PropertyBuilder(TParameters parameters)
	{
		_parameters = parameters;
	}

	public ClassProperty<TParameters, TProperty> ClassProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : class
	{
		var classProperty = new ClassProperty<TParameters, TProperty>(_parameters);
		_properties.Add(classProperty);
		return classProperty;
	}

	public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : struct
	{
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters);
		_properties.Add(structProperty);
		return structProperty;
	}

	public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Func<TResult, TProperty> property)
		where TProperty : struct
	{
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters);
		_properties.Add(structProperty);
		return structProperty;
	}

	public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : struct
	{
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters);
		_properties.Add(enumProperty);
		return enumProperty;
	}

	public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Func<TResult, TProperty> property)
		where TProperty : struct
	{
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters);
		_properties.Add(enumProperty);
		return enumProperty;
	}

	public ListProperty<TParameters, TProperty> ListProperty<TProperty>(Func<TResult, IEnumerable<TProperty>?> property)
		where TProperty : notnull
	{
		var classListProperty = new ListProperty<TParameters, TProperty>(_parameters);
		_properties.Add(classListProperty);
		return classListProperty;
	}
}
