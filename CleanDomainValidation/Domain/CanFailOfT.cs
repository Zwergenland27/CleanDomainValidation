
namespace CleanDomainValidation.Domain;

public sealed class CanFail<TResult> : AbstractCanFail, ICanFail<TResult>
{
	private TResult _value = default!;

	private bool _valueSet = false;

	public static InvalidOperationException ValueNotSet => new ("The value has not been set yet");

	public TResult Value
	{
		get
		{
			if (_errors.Count != 0) throw new ValueInvalidException();
			if (!_valueSet) throw new ValueNotSetException();
			return _value;
		}
	}


	public void Succeeded(TResult value)
	{
		_value = value;
		_valueSet = true;
	}

	/// <summary>
	/// Creates successfull <see cref="CanFail{TResult}"/> înstance containing the <paramref name="value"/>
	/// </summary>
	/// <remarks>
	/// This is what the implicit conversion from <typeparamref name="TResult"/> to <see cref="CanFail{TResult}"/> does
	/// </remarks>
	public static CanFail<TResult> Success(TResult value)
	{
		var canFail = new CanFail<TResult>();
		canFail.Succeeded(value);
		return canFail;
	}

	/// <summary>
	/// Create <see cref="CanFail{TResult}"/> instance containing the <paramref name="error"/>
	/// </summary>
	/// <remarks>
	/// This is what the implicit conversion from <see cref="Error"/> to <see cref="CanFail{TResult}"/> does
	/// </remarks>
	public static CanFail<TResult> FromError(Error error)
	{
		var canFail = new CanFail<TResult>();
		canFail.Failed(error);
		return canFail;
	}

	/// <summary>
	/// Create <see cref="CanFail"/> instance from a class of <see cref="AbstractCanFail"/> that has failed
	/// </summary>
	public static CanFail<TResult> FromFailure(AbstractCanFail result)
	{
		if (!result.HasFailed)
		{
			throw new NoErrorsOccuredException($"Cannot use CanFail<{typeof(TResult)}>.FromFailure on a result that has not failed");
		}

		var canFail = new CanFail<TResult>();
		canFail.InheritFailure(result);
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
}
