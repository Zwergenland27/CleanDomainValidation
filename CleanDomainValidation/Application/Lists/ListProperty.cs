using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

public sealed class ListProperty<TParameters, TProperty> : IValidatableProperty
{
	private IValidatableProperty _property;
	private TParameters _parameters;

	public CanFail ValidationResult => _property.ValidationResult;

	internal ListProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

	public RequiredListProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredListProperty<TParameters, TProperty>(_parameters, missingError);
		_property = required;
		return required;
	}

	public OptionalListProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalListProperty<TParameters, TProperty>(_parameters);
		_property = optional;
		return optional;
	}
}
