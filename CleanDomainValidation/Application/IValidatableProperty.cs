using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

/// <summary>
/// A property of a request that can be validated
/// </summary>
public interface IValidatableProperty
{
	/// <summary>
	/// Indicates that the property is required and an error will be set if it is missing
	/// </summary>
	bool IsRequired { get; }

	/// <summary>
	/// Indicates that the property is null
	/// </summary>
	bool IsMissing { get; }

	/// <summary>
	/// Contains the errors after the property has been validated
	/// </summary>
	CanFail ValidationResult { get; }
}
