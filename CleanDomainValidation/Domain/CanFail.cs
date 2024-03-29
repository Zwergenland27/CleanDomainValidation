﻿namespace CleanDomainValidation.Domain;

/// <summary>
/// Can be used instead of returning void if an error can occur in the method execution
/// </summary>
public sealed class CanFail : AbstractCanFail
{
	/// <summary>
	/// Converts failure result to <see cref="CanFail{TResult}"/> with <typeparamref name="TOther"/> being the result type
	/// </summary>
	public CanFail<TOther> GetFailureAs<TOther>()
	{
		return CanFail<TOther>.FromFailure(this);
	}

	/// <summary>
	/// Creates successfull <see cref="CanFail"/> instance
	/// </summary>
	public static CanFail Success()
	{
		return new CanFail();
	}

	/// <summary>
	/// Create <see cref="CanFail"/> instance containing the <paramref name="error"/>
	/// </summary>
	/// <remarks>
	/// This what the implicit conversion from <see cref="Error"/> to <see cref="CanFail"/> does
	/// </remarks>
	public static CanFail FromError(Error error)
	{
		var canFail = new CanFail();
		canFail.Failed(error);
		return canFail;
	}

 	/// <summary>
	/// Create <see cref="CanFail"/> instance from a class of <see cref="AbstractCanFail"/> that has failed
	/// </summary>
	public static CanFail FromFailure(AbstractCanFail result)
	{
		if(!result.HasFailed)
		{
			throw new NoErrorsOccuredException("Cannot use CanFail.FromFailure on a result that has not failed");
		}

		var canFail = new CanFail();
		canFail.InheritFailure(result);
		return canFail;
	}

	/// <summary>
	/// Create <see cref="CanFail"/> instance from <see cref="Error"/>
	/// </summary>
	public static implicit operator CanFail(Error error)
	{
		return FromError(error);
	}
}
