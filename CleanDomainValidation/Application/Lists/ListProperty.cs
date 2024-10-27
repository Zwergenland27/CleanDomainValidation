using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Lists;

/// <summary>
/// The property is a list
/// </summary>

public sealed class ListProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : notnull
{
	private readonly TParameters _parameters;

	internal ListProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

    /// <summary>
    /// The property cannot be null
    /// </summary>
    /// <param name="missingError">Error that occurs if the property is not set in the request</param>

    public RequiredListProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredListProperty<TParameters, TProperty>(_parameters, missingError);
		Property = required;
		return required;
	}

    /// <summary>
    /// The property can be null
    /// </summary>
    /// <returns></returns>
    public OptionalListProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalListProperty<TParameters, TProperty>(_parameters);
		Property = optional;
		return optional;
	}
}
