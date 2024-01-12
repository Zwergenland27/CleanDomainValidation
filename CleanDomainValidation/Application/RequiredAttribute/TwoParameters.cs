using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public partial class Validator<TCommand>
{
	protected TValueObject RequiredAttribute<T1, T2, TValueObject>(
		T1? param1, Error param1MissingError,
		T2? param2, Error param2MissingError,
		Func<T1, T2, TValueObject> createMethod)
		where T1 : notnull
		where T2 : notnull
	{
		List<Error> missingErrors = new();
		if (param1 is null)
		{
			missingErrors.Add(param1MissingError);
		}

		if (param2 is null)
		{
			missingErrors.Add(param2MissingError);
		}

		if (missingErrors.Any())
		{
			_validationErrors.AddRange(missingErrors);
			return default!;
		}

		return createMethod.Invoke(param1!, param2!);
	}

	protected TValueObject RequiredAttribute<T1, T2, TValueObject>(
		T1? param1, Error param1MissingError,
		T2? param2, Error param2MissingError,
		Func<T1, T2, TValueObject> createMethod)
		where T1 : struct
		where T2 : notnull
	{
		List<Error> missingErrors = new();
		if (param1 is null)
		{
			missingErrors.Add(param1MissingError);
		}

		if (param2 is null)
		{
			missingErrors.Add(param2MissingError);
		}

		if (missingErrors.Any())
		{
			_validationErrors.AddRange(missingErrors);
			return default!;
		}

		return createMethod.Invoke(param1!.Value, param2!);
	}

	protected TValueObject RequiredAttribute<T1, T2, TValueObject>(
		T1? param1, Error param1MissingError,
		T2? param2, Error param2MissingError,
		Func<T1, T2, TValueObject> createMethod)
		where T1 : notnull
		where T2 : struct
	{
		List<Error> missingErrors = new();
		if (param1 is null)
		{
			missingErrors.Add(param1MissingError);
		}

		if (param2 is null)
		{
			missingErrors.Add(param2MissingError);
		}

		if (missingErrors.Any())
		{
			_validationErrors.AddRange(missingErrors);
			return default!;
		}

		return createMethod.Invoke(param1!, param2!.Value);
	}

	protected TValueObject RequiredAttribute<T1, T2, TValueObject>(
		T1? param1, Error param1MissingError,
		T2? param2, Error param2MissingError,
		Func<T1, T2, TValueObject> createMethod)
		where T1 : struct
		where T2 : struct
	{
		List<Error> missingErrors = new();
		if (param1 is null)
		{
			missingErrors.Add(param1MissingError);
		}

		if (param2 is null)
		{
			missingErrors.Add(param2MissingError);
		}

		if (missingErrors.Any())
		{
			_validationErrors.AddRange(missingErrors);
			return default!;
		}

		return createMethod.Invoke(param1!.Value, param2!.Value);
	}

	protected TValueObject RequiredAttribute<T1, T2, TValueObject>(
		T1? param1, Error param1MissingError,
		T2? param2, Error param2MissingError,
		Func<T1, T2, CanFail<TValueObject>> createMethod)
		where T1 : notnull
		where T2 : notnull
	{
		List<Error> missingErrors = new();
		if (param1 is null)
		{
			missingErrors.Add(param1MissingError);
		}

		if (param2 is null)
		{
			missingErrors.Add(param2MissingError);
		}

		if (missingErrors.Any())
		{
			_validationErrors.AddRange(missingErrors);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1!, param2!);
		if (result.HasFailed)
		{
			_validationErrors.AddRange(result.Errors);
			return default!;
		}

		return result.Value;
	}

	protected TValueObject RequiredAttribute<T1, T2, TValueObject>(
		T1? param1, Error param1MissingError,
		T2? param2, Error param2MissingError,
		Func<T1, T2, CanFail<TValueObject>> createMethod)
		where T1 : struct
		where T2 : notnull
	{
		List<Error> missingErrors = new();
		if (param1 is null)
		{
			missingErrors.Add(param1MissingError);
		}

		if (param2 is null)
		{
			missingErrors.Add(param2MissingError);
		}

		if (missingErrors.Any())
		{
			_validationErrors.AddRange(missingErrors);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1!.Value, param2!);
		if (result.HasFailed)
		{
			_validationErrors.AddRange(result.Errors);
			return default!;
		}

		return result.Value;
	}

	protected TValueObject RequiredAttribute<T1, T2, TValueObject>(
		T1? param1, Error param1MissingError,
		T2? param2, Error param2MissingError,
		Func<T1, T2, CanFail<TValueObject>> createMethod)
		where T1 : notnull
		where T2 : struct
	{
		List<Error> missingErrors = new();
		if (param1 is null)
		{
			missingErrors.Add(param1MissingError);
		}

		if (param2 is null)
		{
			missingErrors.Add(param2MissingError);
		}

		if (missingErrors.Any())
		{
			_validationErrors.AddRange(missingErrors);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1!, param2!.Value);
		if (result.HasFailed)
		{
			_validationErrors.AddRange(result.Errors);
			return default!;
		}

		return result.Value;
	}

	protected TValueObject RequiredAttribute<T1, T2, TValueObject>(
		T1? param1, Error param1MissingError,
		T2? param2, Error param2MissingError,
		Func<T1, T2, CanFail<TValueObject>> createMethod)
		where T1 : struct
		where T2 : struct
	{
		List<Error> missingErrors = new();
		if (param1 is null)
		{
			missingErrors.Add(param1MissingError);
		}

		if (param2 is null)
		{
			missingErrors.Add(param2MissingError);
		}

		if (missingErrors.Any())
		{
			_validationErrors.AddRange(missingErrors);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1!.Value, param2!.Value);
		if (result.HasFailed)
		{
			_validationErrors.AddRange(result.Errors);
			return default!;
		}

		return result.Value;
	}
}
