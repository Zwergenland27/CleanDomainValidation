using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

/// <summary>
/// The property is a list
/// </summary>

public sealed class ListProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : notnull
{
	private readonly TParameters _parameters;

	internal ListProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

    /// <summary>
    /// The property cannot be null
    /// </summary>
    /// <param name="missingError">Error that occurs if the property is not set in the request</param>

    public RequiredListProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredListProperty<TParameters, TProperty>(_parameters, missingError);
		Property = required;
		return required;
	}
    
	/// <summary>
	/// The property will be set with the default list if the parameter is not set
	/// </summary>
	/// <param name="defaultValue">Default list that should be set if parameter is null</param>
	public RequiredListWithDefaultProperty<TParameters, TProperty> WithDefault(IEnumerable<TProperty> defaultValue)
	{
		var requiredWithDefault = new RequiredListWithDefaultProperty<TParameters, TProperty>(_parameters, defaultValue);
		Property = requiredWithDefault;
		return requiredWithDefault;
	}

    /// <summary>
    /// The property can be null
    /// </summary>
    /// <returns></returns>
    public OptionalListProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalListProperty<TParameters, TProperty>(_parameters);
		Property = optional;
		return optional;
	}
}
