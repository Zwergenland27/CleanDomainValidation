﻿using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

/// <summary>
/// The property is an enum that cannot be null
/// </summary>
public sealed class RequiredEnumProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	internal Error MissingError { get; }
	internal TParameters Parameters { get; }
	internal NamingStack NamingStack { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal RequiredEnumProperty(TParameters parameters, Error missingError, NamingStack namingStack)
	{
		Parameters = parameters;
		MissingError = missingError;
		NamingStack = namingStack;
	}
}