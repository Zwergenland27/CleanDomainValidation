using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

/// <summary>
/// The property is an enum that can be null
/// </summary>
public sealed class OptionalEnumProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : struct
{
	internal TParameters Parameters { get; }
	internal NamingStack NamingStack { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal OptionalEnumProperty(TParameters parameters, NamingStack namingStack)
	{
		Parameters = parameters;
		NamingStack = namingStack;
	}
}
