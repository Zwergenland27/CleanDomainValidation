using CleanDomainValidation.Domain;
using System.Net.NetworkInformation;

namespace CleanDomainValidation.Application.Classes;

public sealed class RequiredClassProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : class
{
	public Error MissingError { get; }
	public TParameters Parameters { get; }
	public CanFail ValidationResult { get; } = new();

	internal RequiredClassProperty(TParameters parameters, Error missingError)
	{
		Parameters = parameters;
		MissingError = missingError;
	}
}
