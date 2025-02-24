﻿using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Structs;

/// <summary>
/// The property is a struct
/// </summary>

public sealed class StructProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : struct
{
	private readonly TParameters _parameters;
	private readonly NamingStack _namingStack;

	internal StructProperty(TParameters parameters, NamingStack namingStack)
	{
		_parameters = parameters;
		_namingStack = namingStack;
	}

    /// <summary>
    /// The property cannot be null. In case the parameter is missing, <paramref name="missingError"/> will be set
    /// </summary>
    /// <param name="missingError">Error that occurs if the property is not set in the request</param>
    public RequiredStructProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredStructProperty<TParameters, TProperty>(_parameters, missingError, _namingStack);
		Property = required;
		return required;
	}
    
	/// <summary>
	/// The property cannot be null. In case the parameter is missing an error will be automatically generated
	/// </summary>
	/// <param name="customErrorMessage">Custom error message for the missing parameter error</param>
	public RequiredStructProperty<TParameters, TProperty> Required(string? customErrorMessage = null)
	{
		var error = Error.Validation(_namingStack.ErrorCode, customErrorMessage ?? _namingStack.ErrorMessage);
		var required = new RequiredStructProperty<TParameters, TProperty>(_parameters, error, _namingStack);
		Property = required;
		return required;
	}
    
	/// <summary>
	/// The property will be set with the default value if the parameter is not set
	/// </summary>
	/// <param name="defaultValue">Default value that should be set if parameter is null</param>
	public RequiredStructWithDefaultProperty<TParameters, TProperty> WithDefault(TProperty defaultValue)
	{
		var requiredWithDefault = new RequiredStructWithDefaultProperty<TParameters, TProperty>(_parameters, defaultValue, _namingStack);
		Property = requiredWithDefault;
		return requiredWithDefault;
	}

    /// <summary>
    /// The property can be null
    /// </summary>
    /// <returns></returns>
    public OptionalStructProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalStructProperty<TParameters, TProperty>(_parameters, _namingStack);
		Property = optional;
		return optional;
	}
}
