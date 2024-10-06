using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Enums;

/// <summary>
/// The property is an enum
/// </summary>
public sealed class EnumProperty<TParameters, TProperty> : ValidatableBaseProperty
	where TParameters : notnull
	where TProperty : struct
{
	private readonly TParameters _parameters;

	internal EnumProperty(TParameters parameters)
	{
		_parameters = parameters;
	}

    /// <summary>
    /// The property cannot be null
    /// </summary>
    /// <param name="missingError">Error that occurs if the property is not set in the request</param>
    public RequiredEnumProperty<TParameters, TProperty> Required(Error missingError)
	{
		var required = new RequiredEnumProperty<TParameters, TProperty>(_parameters, missingError);
		Property = required;
		return required;
	}

    /// <summary>
    /// The property can be null
    /// </summary>
    /// <returns></returns>
    public OptionalEnumProperty<TParameters, TProperty> Optional()
	{
		var optional = new OptionalEnumProperty<TParameters, TProperty>(_parameters);
		Property = optional;
		return optional;
	}
}
