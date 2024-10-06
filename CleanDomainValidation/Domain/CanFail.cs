namespace CleanDomainValidation.Domain;

/// <summary>
/// Can be used instead of returning void if an error can occur in the method execution
/// </summary>
public sealed class CanFail : AbstractCanFail
{
	/// <summary>
	/// Creates successfull <see cref="CanFail"/> instance
	/// </summary>

	public static CanFail Success => new();

	/// <summary>
	/// Create <see cref="CanFail"/> instance containing the <paramref name="error"/>
	/// </summary>
	public static CanFail FromError(Error error)
	{
		var canFail = new CanFail();
		canFail.Failed(error);
		return canFail;
	}

	/// <summary>
	/// Create <see cref="CanFail"/> instance from errors returned by another <see cref="AbstractCanFail"/> object
	/// </summary>
	/// <param name="errors">List of the errors of the other <see cref="AbstractCanFail"/> object</param>
	public static CanFail FromErrors(ReadOnlyErrorCollection errors)
	{
		var canFail = new CanFail();
		canFail.Failed(errors);
		return canFail;
	}

	/// <summary>
	/// Create <see cref="CanFail"/> instance from <see cref="Error"/>
	/// </summary>
	public static implicit operator CanFail(Error error)
	{
		return FromError(error);
	}

	/// <summary>
	/// Create <see cref="CanFail"/> instance from errors returned by another <see cref="AbstractCanFail"/> object
	/// </summary>
	/// <param name="errors">List of the errors of the other <see cref="AbstractCanFail"/> object</param>
	public static implicit operator CanFail(ReadOnlyErrorCollection errors)
	{
		return FromErrors(errors);
	}
}
