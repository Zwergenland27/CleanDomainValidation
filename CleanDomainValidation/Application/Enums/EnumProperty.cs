using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

public sealed class EnumProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : struct
{
	private readonly TParameters _parameters;

	internal EnumProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

	public RequiredEnumProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredEnumProperty<TParameters, TProperty>(_parameters, missingError);
		Property = required;
		return required;
	}

	public OptionalEnumProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalEnumProperty<TParameters, TProperty>(_parameters);
		Property = optional;
		return optional;
	}
}
