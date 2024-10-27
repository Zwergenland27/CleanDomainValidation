using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

/// <summary>
/// Property that can be validated
/// </summary>
public abstract class ValidatableBaseProperty : ValidatableProperty
{
    /// <summary>
    /// The property that is being validated
    /// </summary>
    protected ValidatableProperty? Property;

    internal override CanFail ValidationResult
    {
        get
        {
            if(Property is null)
            {
                throw new InvalidOperationException("The property is not set");
            }

            return Property.ValidationResult;
        }
    }
}
