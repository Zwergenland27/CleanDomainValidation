﻿using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Struct;

public sealed class StructProperty<TParameters, TProperty> : IValidatableProperty
	where TProperty : struct
{
	private IValidatableProperty _property;
	private TParameters _parameters;
	public CanFail ValidationResult => _property.ValidationResult;

	internal StructProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

	public RequiredStructProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredStructProperty<TParameters, TProperty>(_parameters, missingError);
		_property = required;
		return required;
	}

	public OptionalStructProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalStructProperty<TParameters, TProperty>(_parameters);
		_property = optional;
		return optional;
	}
}
