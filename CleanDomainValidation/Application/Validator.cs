using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public abstract partial class Validator<TCommand>
	where TCommand : ICommand
{
	private protected readonly List<Error> _validationErrors = new();

	private Func<TCommand>? _creationMethod;

	protected IReadOnlyList<Error> ValidationErrors => _validationErrors.AsReadOnly();

	protected void CreateInstance(Func<TCommand> creationMethod)
	{
		_creationMethod = creationMethod;
	}

	public CanFail<TCommand> Validate()
	{
		if (_creationMethod is null)
		{
			throw new InvalidOperationException("You have to specify the command creation method using the CreateInstance method.");
		}

		if (_validationErrors.Any())
		{
			return CanFail<TCommand>.FromErrors(_validationErrors);
		}

		return _creationMethod.Invoke();
	}
}
