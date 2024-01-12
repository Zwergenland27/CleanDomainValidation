namespace CleanDomainValidation.Domain;

/// <summary>
/// Can be used instead of returning void if an error can occur in the method execution
/// </summary>
public interface ICanFail
{
	/// <summary>
	/// Gets the list of errors
	/// </summary>
	/// <exception cref="NoErrorsOccuredException">Property is accessed when no errors occured</exception>
	IReadOnlyList<Error> Errors { get; }

	/// <summary>
	/// Indicates that one ore more errors have occured
	/// </summary>
	public bool HasFailed { get; }

	/// <summary>
	/// Describes the current state
	/// </summary>
	public FailureType Type { get; } 
}
