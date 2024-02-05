namespace CleanDomainValidation.Domain;

/// <summary>
/// Error that occurs when value is accessed that has not been set yet
/// </summary>
public class ValueNotSetException : InvalidOperationException
{
	/// <summary>
	/// Initializes a new instance of <see cref="ValueNotSetException"/>
	/// </summary>
	public ValueNotSetException() : base("The value of the result has not been set yet") { }
}
