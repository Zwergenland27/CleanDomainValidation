using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Structs;

/// <summary>
/// The property is a struct
/// </summary>

public sealed class StructProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : struct
{
	private readonly TParameters _parameters;

	internal StructProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

    /// <summary>
    /// The property cannot be null
    /// </summary>
    /// <param name="missingError">Error that occurs if the property is not set in the request</param>
    public RequiredStructProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredStructProperty<TParameters, TProperty>(_parameters, missingError);
		Property = required;
		return required;
	}
    
	/// <summary>
	/// The property will be set with the default value if the parameter is not set
	/// </summary>
	/// <param name="defaultValue">Default value that should be set if parameter is null</param>
	public RequiredStructWithDefaultProperty<TParameters, TProperty> WithDefault(TProperty defaultValue)
	{
		var requiredWithDefault = new RequiredStructWithDefaultProperty<TParameters, TProperty>(_parameters, defaultValue);
		Property = requiredWithDefault;
		return requiredWithDefault;
	}

    /// <summary>
    /// The property can be null
    /// </summary>
    /// <returns></returns>
    public OptionalStructProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalStructProperty<TParameters, TProperty>(_parameters);
		Property = optional;
		return optional;
	}
}
