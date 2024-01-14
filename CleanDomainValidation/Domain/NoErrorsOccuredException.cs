namespace CleanDomainValidation.Domain;

public class NoErrorsOccuredException(string? message = null) : InvalidOperationException(message)
{
}
