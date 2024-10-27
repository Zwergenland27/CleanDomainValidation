using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;

namespace CleanDomainValidation.Application;

/// <summary>
/// Builder for creating <typeparamref name="TResult"/> mapped from parameters of type <typeparamref name="TParameters"/>
/// </summary>
public abstract class PropertyBuilder<TParameters, TResult>
	where TParameters : notnull
	where TResult : notnull
{
	private readonly TParameters _parameters;
	private readonly List<ValidatableProperty> _properties = [];

    /// <summary>
    /// List of properties that have been added to the builder and will be validated
    /// </summary>
    protected IReadOnlyList<ValidatableProperty> Properties => _properties.AsReadOnly();

	internal PropertyBuilder(TParameters parameters)
	{
		_parameters = parameters;
	}

	/// <summary>
	/// Map the class property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
	/// </summary>
	/// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
	public ClassProperty<TParameters, TProperty> ClassProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : class
	{
		var classProperty = new ClassProperty<TParameters, TProperty>(_parameters);
		_properties.Add(classProperty);
		return classProperty;
	}

    /// <summary>
    /// Map the struct property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : struct
	{
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters);
		_properties.Add(structProperty);
		return structProperty;
	}

    /// <summary>
    /// Map the struct property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Func<TResult, TProperty> property)
		where TProperty : struct
	{
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters);
		_properties.Add(structProperty);
		return structProperty;
	}

    /// <summary>
    /// Map the enum property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Func<TResult, TProperty?> property)
		where TProperty : struct
	{
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters);
		_properties.Add(enumProperty);
		return enumProperty;
	}

    /// <summary>
    /// Map the enum property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Func<TResult, TProperty> property)
		where TProperty : struct
	{
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters);
		_properties.Add(enumProperty);
		return enumProperty;
	}

    /// <summary>
    /// Map the list property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    public ListProperty<TParameters, TProperty> ListProperty<TProperty>(Func<TResult, IEnumerable<TProperty>?> property)
		where TProperty : notnull
	{
		var classListProperty = new ListProperty<TParameters, TProperty>(_parameters);
		_properties.Add(classListProperty);
		return classListProperty;
	}
}
