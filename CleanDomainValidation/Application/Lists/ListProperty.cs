using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

public sealed class ListProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : notnull
{
	private readonly TParameters _parameters;

	internal ListProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

	public RequiredListProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredListProperty<TParameters, TProperty>(_parameters, missingError);
		Property = required;
		return required;
	}

	public OptionalListProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalListProperty<TParameters, TProperty>(_parameters);
		Property = optional;
		return optional;
	}
}
