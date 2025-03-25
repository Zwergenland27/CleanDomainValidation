using System.Linq.Expressions;
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
	private readonly NameStack _nameStack;

    /// <summary>
    /// List of properties that have been added to the builder and will be validated
    /// </summary>
    protected IReadOnlyList<ValidatableProperty> Properties => _properties.AsReadOnly();

	internal PropertyBuilder(TParameters parameters, NameStack nameStack)
	{
		_parameters = parameters;
		_nameStack = nameStack;
	}

	/// <summary>
	/// Map the class property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
	/// </summary>
	/// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
	/// <param name="name">Optional name that's used for the error code</param>
	public ClassProperty<TParameters, TProperty> ClassProperty<TProperty>(Expression<Func<TResult, TProperty?>> property, string? name = null)
		where TProperty : class
	{
		if (name is null) name = GetPropertyName(property);
		_nameStack.PushProperty(name);
		var classProperty = new ClassProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(classProperty);
		return classProperty;
	}
	
	/// <summary>
	/// Map the class property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
	/// </summary>
	/// <param name="name">Name that's used for the error code</param>
	public ClassProperty<TParameters, TProperty> ClassProperty<TProperty>(string name)
		where TProperty : class
	{
		_nameStack.PushProperty(name);
		var classProperty = new ClassProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(classProperty);
		return classProperty;
	}

    /// <summary>
    /// Map the struct property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    /// <param name="name">Optional name that's used for the error code</param>
    public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Expression<Func<TResult, TProperty?>> property, string? name = null)
		where TProperty : struct
	{
		if (name is null) name = GetPropertyName(property);
		_nameStack.PushProperty(name);
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(structProperty);
		return structProperty;
	}

    /// <summary>
    /// Map the struct property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    /// <param name="name">Optional name that's used for the error code</param>
    public StructProperty<TParameters, TProperty> StructProperty<TProperty>(Expression<Func<TResult, TProperty>> property, string? name = null)
		where TProperty : struct
	{
		if (name is null) name = GetPropertyName(property);
		_nameStack.PushProperty(name);
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(structProperty);
		return structProperty;
	}
    
	/// <summary>
	/// Map the struct property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
	/// </summary>
	/// <param name="name">Name that's used for the error code</param>
	public StructProperty<TParameters, TProperty> StructProperty<TProperty>(string name)
		where TProperty : struct
	{
		_nameStack.PushProperty(name);
		var structProperty = new StructProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(structProperty);
		return structProperty;
	}

    /// <summary>
    /// Map the enum property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    /// <param name="name">Optional name that's used for the error code</param>
    public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Expression<Func<TResult, TProperty?>> property, string? name = null)
		where TProperty : struct
	{
		if (name is null) name = GetPropertyName(property);
		_nameStack.PushProperty(name);
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(enumProperty);
		return enumProperty;
	}

    /// <summary>
    /// Map the enum property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    /// <param name="name">Optional name that's used for the error code</param>
    public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(Expression<Func<TResult, TProperty>> property, string? name = null)
		where TProperty : struct
	{
		if (name is null) name = GetPropertyName(property);
		_nameStack.PushProperty(name);
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(enumProperty);
		return enumProperty;
	}
    
	/// <summary>
	/// Map the enum property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
	/// </summary>
	/// <param name="name">Name that's used for the error code</param>
	public EnumProperty<TParameters, TProperty> EnumProperty<TProperty>(string name)
		where TProperty : struct
	{
		_nameStack.PushProperty(name);
		var enumProperty = new EnumProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(enumProperty);
		return enumProperty;
	}

    /// <summary>
    /// Map the list property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="property">Expression to get type of the specified property of <typeparamref name="TResult"/></param>
    /// <param name="name">Optional name that's used for the error code</param>
    public ListProperty<TParameters, TProperty> ListProperty<TProperty>(Expression<Func<TResult, IEnumerable<TProperty>?>> property, string? name = null)
		where TProperty : notnull
	{
		if (name is null) name = GetPropertyName(property);
		_nameStack.PushProperty(name);
		var classListProperty = new ListProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(classListProperty);
		return classListProperty;
	}
    
	/// <summary>
	/// Map the list property <typeparamref name="TProperty"/> of the result <typeparamref name="TResult"/>
	/// </summary>
	/// <param name="name">Name that's used for the error code</param>
	public ListProperty<TParameters, TProperty> ListProperty<TProperty>(string name)
		where TProperty : notnull
	{
		_nameStack.PushProperty(name);
		var classListProperty = new ListProperty<TParameters, TProperty>(_parameters, _nameStack);
		_properties.Add(classListProperty);
		return classListProperty;
	}

	private static string GetPropertyName<T1, T2>(Expression<Func<T1, T2>> expression)
	{
		Expression body = expression.Body;
		
		while(body is UnaryExpression unaryExpression)
		{
			body =  unaryExpression.Operand;
		}
		
		if (body is ParameterExpression)
		{
			throw new InvalidOperationException("Please provide a custom name when using a self referencing property.");
		}

		if (body is MemberExpression member)
		{
			return member.Member.Name;
		}
		
		throw new InvalidOperationException("Expression is not referencing a property");
	}
}
