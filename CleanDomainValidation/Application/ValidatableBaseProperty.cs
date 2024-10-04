using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public abstract class ValidatableBaseProperty : ValidatableProperty
{
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
