namespace CleanDomainValidation.Domain;

/// <summary>
/// Description of the current state of the <see cref="ICanFail"/> object
/// </summary>
public enum FailureType
{
	/// <summary>
	/// No errors occured
	/// </summary>
	None,
	/// <summary>
	/// One error occured
	/// </summary>
	One,
	/// <summary>
	/// Many errors of the same type occured
	/// </summary>
	Many,
	/// <summary>
	/// Many erros of at least two types occured
	/// </summary>
	ManyDifferent
}
