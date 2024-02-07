using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public interface IValidatableProperty
{
	internal abstract CanFail ValidationResult { get; }
}
