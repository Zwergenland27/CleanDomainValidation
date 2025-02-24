namespace CleanDomainValidation.Domain;

/// <summary>
/// Error that occurs when a value is accessed and errors where thrown
/// </summary>
public class ValueInvalidException : InvalidOperationException
{
	/// <summary>
	/// Initializes a new instance of <see cref="ValueNotSetException"/>
	/// </summary>
	public ValueInvalidException(IEnumerable<Error> occuredErrors) : base(
		"The value of the result cannot be accessed because following error(s) occured:\n" +
		$"{ListAllErrorCodes(occuredErrors)}\n" +
		"") { }

	private static string ListAllErrorCodes(IEnumerable<Error> errors)
	{
		var result = "";

		foreach (var error in errors)
		{
			result += error.Code + "\n";
		}
		
		return result;
	}
}
