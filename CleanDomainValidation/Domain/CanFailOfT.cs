
namespace CleanDomainValidation.Domain;

/// <summary>
/// Can be used instead of returning <typeparamref name="TResult"/> if an error can occur in the method execution
/// </summary>
public sealed class CanFail<TResult> : AbstractCanFail, ICanFail<TResult>
{
	private TResult _value = default!;

	private bool _valueSet = false;

	/// <summary>
	/// Error that occurs when the value is accessed but not set yet
	/// </summary>
	public static InvalidOperationException ValueNotSet => new ("The value of the result object has not been set yet");

	/// <summary>
	/// Access the value that should be returned normally
	/// </summary>
	/// <remarks>
	/// This property can only be accessed if the value is set and no errors occured
	/// </remarks>
	/// <exception cref="ValueInvalidException">
	/// The value is set but errors have occured
	/// </exception>
	/// <exception cref="ValueNotSetException">
	/// The value is not set
	/// </exception>
	public TResult Value
	{
		get
		{
			if (HasFailed) throw new ValueInvalidException();
			if (!_valueSet) throw new ValueNotSetException();
			return _value;
		}
	}

	/// <summary>
	/// Assigns <paramref name="value"/> of <typeparamref name="TResult"/> to value of the result
	/// </summary>
	/// <remarks>
	/// Method can be called after a failure has been added to the result but the value cannot be accessed.
	/// </remarks>
	public void Succeeded(TResult value)
	{
		_value = value;
		_valueSet = true;
	}

	/// <summary>
	/// Creates successfull <see cref="CanFail{TResult}"/> înstance containing the <paramref name="value"/>
	/// </summary>
	public static CanFail<TResult> Success(TResult value)
	{
		var canFail = new CanFail<TResult>();
		canFail.Succeeded(value);
		return canFail;
	}

	/// <summary>
	/// Create <see cref="CanFail{TResult}"/> instance containing the <paramref name="error"/>
	/// </summary>
	public static CanFail<TResult> FromError(Error error)
	{
		var canFail = new CanFail<TResult>();
		canFail.Failed(error);
		return canFail;
	}

	/// <summary>
	/// Create <see cref="CanFail{TResult}"/> instance from errors returned by another <see cref="AbstractCanFail"/> object
	/// </summary>
	/// <param name="errors">List of the errors of the other <see cref="AbstractCanFail"/> object</param>
	public static CanFail<TResult> FromErrors(ReadOnlyErrorCollection errors)
	{
		var canFail = new CanFail<TResult>();
		canFail.Failed(errors);
		return canFail;
	}

	/// <summary>
	/// Create <see cref="CanFail{TResult}"/> instance from <see cref="Error"/>
	/// </summary>
	public static implicit operator CanFail<TResult>(Error error)
	{
		return FromError(error);
	}

	/// <summary>
	/// Create <see cref="CanFail{TResult}"/> instance with valid parameter <paramref name="value"/>
	/// </summary>
	public static implicit operator CanFail<TResult>(TResult value)
	{
		return Success(value);
	}

	/// <summary>
	/// Create <see cref="CanFail{TResult}"/> instance from errors returned by another <see cref="AbstractCanFail"/> object
	/// </summary>
	/// <param name="errors">List of the errors of the other <see cref="AbstractCanFail"/> object</param>
	public static implicit operator CanFail<TResult>(ReadOnlyErrorCollection errors)
	{
		return FromErrors(errors);
	}
}
