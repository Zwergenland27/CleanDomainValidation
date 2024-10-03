﻿using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

public sealed class OptionalListProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : notnull
{
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal OptionalListProperty(TParameters parameters)
	{
		Parameters = parameters;
	}
}
