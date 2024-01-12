using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public partial class Validator<TCommand>
{
	protected TValueObject RequiredAttribute<T1, TValueObject>(
		T1? param1, Error param1MissingError,
		Func<T1, TValueObject> createMethod)
		where T1 : notnull
	{
		if (param1 is null)
		{
			_validationErrors.Add(param1MissingError);
			return default!;
		}

		return createMethod.Invoke(param1);
	}

	protected TValueObject RequiredAttribute<T1, TValueObject>(
		T1? param1, Error param1MissingError,
		Func<T1, TValueObject> createMethod)
		where T1 : struct
	{
		if (param1 is null)
		{
			_validationErrors.Add(param1MissingError);
			return default!;
		}

		return createMethod.Invoke(param1.Value);
	}

	protected TValueObject RequiredAttribute<T1, TValueObject>(
		T1? param1, Error param1MissingError,
		Func<T1, CanFail<TValueObject>> createMethod)
		where T1 : notnull
	{
		if (param1 is null)
		{
			_validationErrors.Add(param1MissingError);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1);
		if (result.HasFailed)
		{
			_validationErrors.AddRange(result.Errors);
			return default!;
		}

		return result.Value;
	}

	protected TValueObject RequiredAttribute<T1, TValueObject>(
		T1? param1, Error param1MissingError,
		Func<T1, CanFail<TValueObject>> createMethod)
		where T1 : struct
	{
		if (param1 is null)
		{
			_validationErrors.Add(param1MissingError);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1.Value);
		if (result.HasFailed)
		{
			_validationErrors.AddRange(result.Errors);
			return default!;
		}

		return result.Value;
	}

}
