﻿using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

public sealed class EnumProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	private IValidatableProperty _property;
	private TParameters _parameters;

	public bool IsRequired => _property.IsRequired;
	public bool IsMissing => _property.IsMissing;
	public CanFail ValidationResult => _property.ValidationResult;

	internal EnumProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

	public RequiredEnumProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredEnumProperty<TParameters, TProperty>(_parameters, missingError);
		_property = required;
		return required;
	}

	public OptionalEnumProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalEnumProperty<TParameters, TProperty>(_parameters);
		_property = optional;
		return optional;
	}
}
