using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

public sealed class ClassProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : class
{
	private readonly TParameters _parameters;

    internal ClassProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

	public RequiredClassProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredClassProperty<TParameters, TProperty>(_parameters, missingError);
		Property = required;
		return required;
	}

	public OptionalClassProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalClassProperty<TParameters, TProperty>(_parameters);
		Property = optional;
		return optional;
	}
}
