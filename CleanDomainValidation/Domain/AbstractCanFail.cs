
namespace CleanDomainValidation.Domain;

public abstract class AbstractCanFail : ICanFail
{
	protected readonly List<Error> _errors = [];

	public IReadOnlyList<Error> Errors => _errors.Count == 0 ? throw new NoErrorsOccuredException() : _errors ;

	public bool HasFailed => _errors.Count != 0;

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

	/// <summary>
	/// Adds the errors of an <see cref="AbstractCanFail"/> object if it has failed
	/// </summary>
	/// <param name="canFail"></param>
	public void InheritFailure(AbstractCanFail canFail)
	{
		if(canFail.HasFailed) _errors.AddRange(canFail.Errors);
	}
}
