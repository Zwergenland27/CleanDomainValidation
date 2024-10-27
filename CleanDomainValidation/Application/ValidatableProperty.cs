using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

/// <summary>
/// Property that can be validated
/// </summary>
public abstract class ValidatableProperty
{
    internal abstract CanFail ValidationResult { get; }
}
