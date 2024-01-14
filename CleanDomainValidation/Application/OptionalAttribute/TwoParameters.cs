using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public partial class Validator<TResult>
{
	protected TValueObject? OptionalAttribute<T1, T2, TValueObject>(
		T1? param1,
		T2? param2,
		Func<T1, T2, TValueObject> createMethod)
		where T1 : notnull
		where T2 : notnull
	{
		if (param1 is null || param2 is null)
		{
			return default;
		}

		return createMethod.Invoke(param1, param2);
	}

	protected TValueObject? OptionalAttribute<T1, T2, TValueObject>(
		T1? param1,
		T2? param2,
		Func<T1, T2, TValueObject> createMethod)
		where T1 : struct
		where T2 : notnull
	{
		if (param1 is null || param2 is null)
		{
			return default;
		}

		return createMethod.Invoke(param1.Value, param2);
	}

	protected TValueObject? OptionalAttribute<T1, T2, TValueObject>(
		T1? param1,
		T2? param2,
		Func<T1, T2, TValueObject> createMethod)
		where T1 : notnull
		where T2 : struct
	{
		if (param1 is null || param2 is null)
		{
			return default;
		}

		return createMethod.Invoke(param1, param2.Value);
	}

	protected TValueObject? OptionalAttribute<T1, T2, TValueObject>(
		T1? param1,
		T2? param2,
		Func<T1, T2, TValueObject> createMethod)
		where T1 : struct
		where T2 : struct
	{
		if (param1 is null || param2 is null)
		{
			return default;
		}

		return createMethod.Invoke(param1.Value, param2.Value);
	}

	protected TValueObject? OptionalAttribute<T1, T2, TValueObject>(
		T1? param1,
		T2? param2,
		Func<T1, T2, CanFail<TValueObject>> createMethod)
		where T1 : notnull
		where T2 : notnull
	{
		if (param1 is null || param2 is null)
		{
			return default;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1, param2);
		if (result.HasFailed)
		{
			_validationResult.InheritFailure(result);
			return default!;
		}

		return result.Value;
	}

	protected TValueObject? OptionalAttribute<T1, T2, TValueObject>(
		T1? param1,
		T2? param2,
		Func<T1, T2, CanFail<TValueObject>> createMethod)
		where T1 : struct
		where T2 : notnull
	{
		if (param1 is null || param2 is null)
		{
			return default;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1.Value, param2);
		if (result.HasFailed)
		{
			_validationResult.InheritFailure(result);
			return default!;
		}

		return result.Value;
	}

	protected TValueObject? OptionalAttribute<T1, T2, TValueObject>(
		T1? param1,
		T2? param2,
		Func<T1, T2, CanFail<TValueObject>> createMethod)
		where T1 : notnull
		where T2 : struct
	{
		if (param1 is null || param2 is null)
		{
			return default;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1, param2.Value);
		if (result.HasFailed)
		{
			_validationResult.InheritFailure(result);
			return default!;
		}

		return result.Value;
	}

	protected TValueObject? OptionalAttribute<T1, T2, TValueObject>(
		T1? param1,
		T2? param2,
		Func<T1, T2, CanFail<TValueObject>> createMethod)
		where T1 : struct
		where T2 : struct
	{
		if (param1 is null || param2 is null)
		{
			return default;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1.Value, param2.Value);
		if (result.HasFailed)
		{
			_validationResult.InheritFailure(result);
			return default!;
		}

		return result.Value;
	}
}
