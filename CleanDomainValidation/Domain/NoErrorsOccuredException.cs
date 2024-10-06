namespace CleanDomainValidation.Domain;
/// <summary>
/// The errors property is accessed when no errors where thrown
/// </summary>
/// <param name="message"></param>
public class NoErrorsOccuredException(string? message = null) : InvalidOperationException(message)
{
}
