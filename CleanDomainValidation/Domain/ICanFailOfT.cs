namespace CleanDomainValidation.Domain;

/// <summary>
/// Can be used instead of returning <typeparamref name="TResult"/> if an error can occur in the method execution
/// </summary>
internal interface ICanFail<out TResult> : ICanFail
{
	/// <summary>
	/// The result value
	/// </summary>
	/// <exception cref="ValueNotSetException">Value is accessed before it has been set</exception>
	/// <exception cref="ValueInvalidException">Value is accessed when errors occured</exception>
	TResult Value { get; }
}
