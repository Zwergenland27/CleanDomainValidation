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
	/// Create <see cref="CanFail"/> instance containing the <paramref name="errors"/>
	/// </summary>
	public static CanFail FromErrors(IEnumerable<Error> errors)
	{
		var canFail = new CanFail();
		foreach (var error in errors)
		{
			canFail.Failed(error);
		}
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
