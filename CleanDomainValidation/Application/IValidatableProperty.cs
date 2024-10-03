using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public interface IValidatableProperty
{
	CanFail ValidationResult { get; }
}
