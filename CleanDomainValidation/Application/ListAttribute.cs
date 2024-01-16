using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public partial class Validator<TResult>
{
	protected IEnumerable<TValueObject> RequiredList<T, TValueObject>(
		IEnumerable<T>? paramList,
		Error missingError,
		Func<T, TValueObject> createMethod,
		Error? emptyError = null)
	{
		if(paramList is null)
		{
			_validationResult.Failed(missingError);
			return null!;
		}

		if(emptyError is not null && paramList.Count() == 0)
		{
			_validationResult.Failed(emptyError);
			return null!;
		}

		return paramList.Select(item => createMethod(item));
	}

	protected IEnumerable<TValueObject> RequiredList<T, TValueObject>(
		IEnumerable<T>? paramList,
		Error missingError,
		Func<T, CanFail<TValueObject>> createMethod,
		Error? emptyError = null)
	{
		if (paramList is null)
		{
			_validationResult.Failed(missingError);
			return null!;
		}

		if (emptyError is not null && !paramList.Any())
		{
			_validationResult.Failed(emptyError);
			return null!;
		}

		List<TValueObject> valueObjects = [];
		foreach(T item in paramList)
		{
			var validateItemResult = createMethod(item);
			if (validateItemResult.HasFailed)
			{
				_validationResult.InheritFailure(validateItemResult);
				continue;
			}

			valueObjects.Add(validateItemResult.Value);
		}

		return valueObjects;
	}

	protected IEnumerable<TValueObject>? OptionalList<T, TValueObject>(
		IEnumerable<T>? paramList,
		Func<T, TValueObject> createMethod,
		Error? emptyError = null)
	{
		if (paramList is null)
		{
			return null;
		}

		if (emptyError is not null && paramList.Count() == 0)
		{
			_validationResult.Failed(emptyError);
			return null!;
		}

		return paramList.Select(item => createMethod(item));
	}

	protected IEnumerable<TValueObject> OptionalList<T, TValueObject>(
		IEnumerable<T>? paramList,
		Func<T, CanFail<TValueObject>> createMethod,
		Error? emptyError = null)
	{
		if (paramList is null)
		{
			return null!;
		}

		if (emptyError is not null && !paramList.Any())
		{
			_validationResult.Failed(emptyError);
			return null!;
		}

		List<TValueObject> valueObjects = [];
		foreach (T item in paramList)
		{
			var validateItemResult = createMethod(item);
			if (validateItemResult.HasFailed)
			{
				_validationResult.InheritFailure(validateItemResult);
				continue;
			}

			valueObjects.Add(validateItemResult.Value);
		}

		return valueObjects;
	}
}
