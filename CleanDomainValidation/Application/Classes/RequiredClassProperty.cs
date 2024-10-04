using CleanDomainValidation.Domain;
using System.Net.NetworkInformation;

namespace CleanDomainValidation.Application.Classes;

public sealed class RequiredClassProperty<TParameters, TProperty> : ValidatableProperty
	where TParameters : notnull
	where TProperty : class
{
	internal Error MissingError { get; }
	internal TParameters Parameters { get; }
	internal override CanFail ValidationResult { get; } = new();

	internal RequiredClassProperty(TParameters parameters, Error missingError)
	{
		Parameters = parameters;
		MissingError = missingError;
	}
}
