using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public abstract class ValidatableProperty
{
    internal abstract CanFail ValidationResult { get; }
}
