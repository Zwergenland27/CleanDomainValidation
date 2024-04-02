using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;

namespace CleanDomainValidation.Application;

/// <summary>
/// Builder for creating an optional validated object of type <typeparamref name="TResult"/> mapped from parameters of type <typeparamref name="TParameters"/>
/// </summary>
public abstract class PropertyBuilder<TParameters, TResult>
	where TParameters : notnull
	where TResult : notnull
{
	private readonly TParameters _parameters;
	private readonly List<IValidatableProperty> _properties = [];

	/// <summary>
	/// List of the properties that the class contains that need to be validated and mapped
	/// </summary>
	protected IReadOnlyList<IValidatableProperty> Properties => _properties.AsReadOnly();

	internal PropertyBuilder(TParameters parameters)
	{
		_parameters = parameters;
	}

	/// <summary>
	/// Class property of type <typeparamref name="TProperty"/> should be mapped
	/// </summary>
	/// <param name="property">Lambda expression of the property</param>
	public ClassProperty<TParameters, TProperty> ClassProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : class
	{
		var classProperty = new ClassProperty<TParameters, TProperty>(_parameters);
		_properties.Add(classProperty);
		return classProperty;
	}

	/// <summary>
	/// Struct property of type <typeparamref name="TProperty"/> should be mapped
	/// </summary>
	/// <param name="property">Lambda expression of the property</param>
	public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : struct
	{
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters);
		_properties.Add(structProperty);
		return structProperty;
	}

	/// <summary>
	/// Struct property of type <typeparamref name="TProperty"/> should be mapped
	/// </summary>
	/// <param name="property">Lambda expression of the property</param>
	public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Func<TResult, TProperty> property)
		where TProperty : struct
	{
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters);
		_properties.Add(structProperty);
		return structProperty;
	}

	/// <summary>
	/// Enum property of type <typeparamref name="TProperty"/> should be mapped
	/// </summary>
	/// <param name="property">Lambda expression of the property</param>
	public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : struct
	{
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters);
		_properties.Add(enumProperty);
		return enumProperty;
	}

	/// <summary>
	/// Enum property of type <typeparamref name="TProperty"/> should be mapped
	/// </summary>
	/// <param name="property">Lambda expression of the property</param>
	public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Func<TResult, TProperty> property)
		where TProperty : struct
	{
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters);
		_properties.Add(enumProperty);
		return enumProperty;
	}

	/// <summary>
	/// List property with List of type <typeparamref name="TProperty"/> should be mapped
	/// </summary>
	/// <param name="property">Lambda expression of the property</param>
	public ListProperty<TParameters, TProperty> ListProperty<TProperty>(Func<TResult, IEnumerable<TProperty>?> property)
		where TProperty : notnull
	{
		var classListProperty = new ListProperty<TParameters, TProperty>(_parameters);
		_properties.Add(classListProperty);
		return classListProperty;
	}
}
