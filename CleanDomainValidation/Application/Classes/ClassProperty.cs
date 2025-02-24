﻿using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

/// <summary>
/// The property is a class
/// </summary>
public sealed class ClassProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : class
{
	private readonly TParameters _parameters;
	private readonly NamingStack _namingStack;

    internal ClassProperty(TParameters parameters, NamingStack namingStack)
	{
		_parameters = parameters;
		_namingStack = namingStack;
	}

	/// <summary>
	/// The property cannot be null. In case the parameter is missing, <paramref name="missingError"/> will be set
	/// </summary>
	/// <param name="missingError">Error that will be set if the property is not set in the request</param>
	public RequiredClassProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredClassProperty<TParameters, TProperty>(_parameters, missingError, _namingStack);
		Property = required;
		return required;
	}
	
	/// <summary>
	/// The property cannot be null. In case the parameter is missing an error will be automatically generated
	/// </summary>
	/// <param name="customErrorMessage">Custom error message for the missing parameter error</param>
	public RequiredClassProperty<TParameters, TProperty> Required(string? customErrorMessage = null)
	{
		var error = Error.Validation(_namingStack.ErrorCode, customErrorMessage ?? _namingStack.ErrorMessage);
		var required = new RequiredClassProperty<TParameters, TProperty>(_parameters, error, _namingStack);
		Property = required;
		return required;
	}
	
	/// <summary>
	/// The property will be set with the default value if the parameter is not set
	/// </summary>
	/// <param name="defaultValue">Default value that should be set if parameter is null</param>
	public RequiredClassWithDefaultProperty<TParameters, TProperty> WithDefault(TProperty defaultValue)
	{
		var requiredWithDefault = new RequiredClassWithDefaultProperty<TParameters, TProperty>(_parameters, defaultValue, _namingStack);
		Property = requiredWithDefault;
		return requiredWithDefault;
	}

	/// <summary>
	/// The property can be null
	/// </summary>
	/// <returns></returns>
	public OptionalClassProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalClassProperty<TParameters, TProperty>(_parameters, _namingStack);
		Property = optional;
		return optional;
	}
}
