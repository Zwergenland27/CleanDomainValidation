using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public interface IValidatableProperty
{
	bool IsRequired { get; }
	bool IsMissing { get; }
	CanFail ValidationResult { get; }
}
