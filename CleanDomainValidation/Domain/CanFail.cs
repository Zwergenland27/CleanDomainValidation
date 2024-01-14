namespace CleanDomainValidation.Domain;

public sealed class CanFail : AbstractCanFail
{
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
			throw new InvalidOperationException("Cannot use CanFail.FromFailure on a result that has not failed");
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
