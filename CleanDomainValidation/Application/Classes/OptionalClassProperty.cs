using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

/// <summary>
/// The property is a class that can be null
/// </summary>
public sealed class OptionalClassProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : class
{
	internal TParameters Parameters { get; }
	
	internal NameStack NameStack { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal OptionalClassProperty(TParameters parameters, NameStack nameStack)
	{
		Parameters = parameters;
		NameStack = nameStack;
	}
}
