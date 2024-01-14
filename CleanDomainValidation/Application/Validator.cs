using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public abstract partial class Validator<TResult>
{
	protected readonly CanFail _validationResult = new();

	private Func<TResult>? _creationMethod;

	protected void CreateInstance(Func<TResult> creationMethod)
	{
		_creationMethod = creationMethod;
	}

	public CanFail<TResult> Validate()
	{
		if (_creationMethod is null)
		{
			throw new InvalidOperationException("You have to specify the command creation method using the CreateInstance method.");
		}

		if (_validationResult.HasFailed)
		{
			return CanFail<TResult>.FromFailure(_validationResult);
		}

		return _creationMethod.Invoke();
	}
}
