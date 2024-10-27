using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

/// <summary>
/// Wrapper for ValidationResult of an required property of type <typeparamref name="TRequest"/>
/// </summary>
/// <remarks>
/// The wrapper is used to ensure that the user cannot return a manually created <see cref="CanFail{TResult}"/> and bypassing the validation logic
/// </remarks>
public sealed class ValidatedRequiredProperty<TRequest>
	where TRequest : notnull
{
	private readonly CanFail<TRequest> _result;

	internal ValidatedRequiredProperty(CanFail<TRequest> result)
	{
		_result = result;
	}

	/// <summary>
	/// Gets the result of the validation
	/// </summary>
	public CanFail<TRequest> Build()
	{
		return _result;
	}
}
