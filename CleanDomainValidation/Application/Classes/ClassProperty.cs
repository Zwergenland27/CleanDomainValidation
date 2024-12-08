using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

/// <summary>
/// The property is a class
/// </summary>
public sealed class ClassProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : class
{
	private readonly TParameters _parameters;

    internal ClassProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

	/// <summary>
	/// The property cannot be null
	/// </summary>
	/// <param name="missingError">Error that occurs if the property is not set in the request</param>
	public RequiredClassProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredClassProperty<TParameters, TProperty>(_parameters, missingError);
		Property = required;
		return required;
	}
	
	/// <summary>
	/// The property will be set with the default value if the parameter is not set
	/// </summary>
	/// <param name="defaultValue">Default value that should be set if parameter is null</param>
	public RequiredClassWithDefaultProperty<TParameters, TProperty> WithDefault(TProperty defaultValue)
	{
		var requiredWithDefault = new RequiredClassWithDefaultProperty<TParameters, TProperty>(_parameters, defaultValue);
		Property = requiredWithDefault;
		return requiredWithDefault;
	}

	/// <summary>
	/// The property can be null
	/// </summary>
	/// <returns></returns>
	public OptionalClassProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalClassProperty<TParameters, TProperty>(_parameters);
		Property = optional;
		return optional;
	}
}
