
namespace CleanDomainValidation.Domain;

/// <summary>
/// Base implementation of <see cref="ICanFail"/>
/// </summary>
public abstract class AbstractCanFail : ICanFail
{
	/// <summary>
	/// List of errros that occured
	/// </summary>
	private readonly List<Error> _errors = [];
	
	/// <inheritdoc/>
	public ReadOnlyErrorCollection Errors => _errors.Count == 0 ? throw new NoErrorsOccuredException() : new ReadOnlyErrorCollection(_errors);
	
	/// <inheritdoc/>
	public bool HasFailed => _errors.Count != 0;

	/// <inheritdoc/>
	public FailureType Type
	{
		get
		{
			if (_errors.Count == 0) return FailureType.None;
			if (_errors.Count == 1) return FailureType.One;

			bool differentErrorFound = false;
			ErrorType errorType1 = _errors[0].Type;
			foreach(Error error in _errors)
			{
                if (error.Type != errorType1)
                {
					differentErrorFound = true;
					break;
                }
            }

			return differentErrorFound ? FailureType.ManyDifferent : FailureType.Many;
		}
	}

	/// <summary>
	/// An error occured
	/// </summary>
	public void Failed(Error error)
	{
		_errors.Add(error);
	}

	internal void Failed(ReadOnlyErrorCollection errors)
	{
		_errors.AddRange(errors);
	}

	/// <summary>
	/// Adds the errors of an <see cref="AbstractCanFail"/> object if it has failed
	/// </summary>
	/// <param name="canFail"></param>
	public void InheritFailure(AbstractCanFail canFail)
	{
		if(canFail.HasFailed) _errors.AddRange(canFail.Errors);
	}
}
