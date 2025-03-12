using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

/// <summary>
/// The property is an enum
/// </summary>
public sealed class EnumProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : struct
{
	private readonly TParameters _parameters;
	private readonly NameStack _nameStack;
	internal EnumProperty(TParameters parameters, NameStack nameStack)
	{
		_parameters = parameters;
		_nameStack = nameStack;
	}

    /// <summary>
    /// The property cannot be null. In case the parameter is missing, <paramref name="missingError"/> will be set
    /// </summary>
    /// <param name="missingError">Error that occurs if the property is not set in the request</param>
    public RequiredEnumProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredEnumProperty<TParameters, TProperty>(_parameters, missingError, _nameStack);
		Property = required;
		return required;
	}
    
	/// <summary>
	/// The property cannot be null. In case the parameter is missing an error will be automatically generated
	/// </summary>
	/// <param name="customErrorMessage">Custom error message for the missing parameter error</param>
	public RequiredEnumProperty<TParameters, TProperty> Required(string? customErrorMessage = null)
	{
		var error = Error.Validation(_nameStack.MissingErrorCode, customErrorMessage ?? _nameStack.MissingErrorMessage);
		var required = new RequiredEnumProperty<TParameters, TProperty>(_parameters, error, _nameStack);
		Property = required;
		return required;
	}
    
	/// <summary>
	/// The property will be set with the default value if the parameter is not set
	/// </summary>
	/// <param name="defaultValue">Default value that should be set if parameter is null</param>
	public RequiredEnumWithDefaultProperty<TParameters, TProperty> Required(TProperty defaultValue)
	{
		var required = new RequiredEnumWithDefaultProperty<TParameters, TProperty>(_parameters, defaultValue, _nameStack);
		Property = required;
		return required;
	}

    /// <summary>
    /// The property can be null
    /// </summary>
    /// <returns></returns>
    public OptionalEnumProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalEnumProperty<TParameters, TProperty>(_parameters, _nameStack);
		Property = optional;
		return optional;
	}
}
