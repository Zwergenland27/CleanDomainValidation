namespace CleanDomainValidation.Domain;

/// <summary>
/// Error that occurs when a value is accessed and errors where thrown
/// </summary>
public class ValueInvalidException : InvalidOperationException
{
	/// <summary>
	/// Initializes a new instance of <see cref="ValueNotSetException"/>
	/// </summary>
	public ValueInvalidException() : base("The value of the result is invalid since more then one error occured") { }
}
