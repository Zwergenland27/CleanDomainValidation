using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

/// <summary>
/// Wrapper for ValidationResult of an optional class property of type <typeparamref name="TRequest"/>
/// </summary>
/// <remarks>
/// The wrapper is used to ensure that the user cannot return a manually created <see cref="CanFail{TResult}"/> and bypassing the validation logic
/// </remarks>
public sealed class ValidatedOptionalClassProperty<TRequest>
	where TRequest : notnull
{
	private readonly CanFail<TRequest?> _result;

	internal ValidatedOptionalClassProperty(CanFail<TRequest?> result)
	{
		_result = result;
	}

	/// <summary>
	/// Gets the result of the validation
	/// </summary>
	public CanFail<TRequest?> Build()
	{
		return _result;
	}
}
