using CleanDomainValidation.Domain;

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
	internal NameStack NameStack { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal RequiredEnumProperty(TParameters parameters, Error missingError, NameStack nameStack)
	{
		Parameters = parameters;
		MissingError = missingError;
		NameStack = nameStack;
	}
}