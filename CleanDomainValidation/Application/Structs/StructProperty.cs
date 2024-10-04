using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Structs;

public sealed class StructProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : struct
{
	private readonly TParameters _parameters;

	internal StructProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

	public RequiredStructProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredStructProperty<TParameters, TProperty>(_parameters, missingError);
		Property = required;
		return required;
	}

	public OptionalStructProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalStructProperty<TParameters, TProperty>(_parameters);
		Property = optional;
		return optional;
	}
}
