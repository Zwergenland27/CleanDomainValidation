using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Classes;

public sealed class ClassProperty<TParameters, TProperty> : IValidatableProperty
	where TParameters : notnull
	where TProperty : class
{
	private IValidatableProperty _property;
	private TParameters _parameters;

	public CanFail ValidationResult => _property.ValidationResult;

	internal ClassProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

	public RequiredClassProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredClassProperty<TParameters, TProperty>(_parameters, missingError);
		_property = required;
		return required;
	}

	public OptionalClassProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalClassProperty<TParameters, TProperty>(_parameters);
		_property = optional;
		return optional;
	}

	//TODO: Add all extension methods in the specific classes and make all properties internal -> not accessible for the user!
}
