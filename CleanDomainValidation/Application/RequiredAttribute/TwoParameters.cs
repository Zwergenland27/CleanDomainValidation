using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public partial class Validator<TResult>
{
	protected TValueObject RequiredAttribute<T1, T2, TValueObject>(
		T1? param1, Error param1MissingError,
		T2? param2, Error param2MissingError,
		Func<T1, T2, TValueObject> createMethod)
		where T1 : notnull
		where T2 : notnull
	{
		CanFail notNullCheck = new();
		if (param1 is null)
		{
			notNullCheck.Failed(param1MissingError);
		}

		if (param2 is null)
		{
			notNullCheck.Failed(param2MissingError);
		}

		if (notNullCheck.HasFailed)
		{
			_validationResult.InheritFailure(notNullCheck);
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
		CanFail notNullCheck = new();
		if (param1 is null)
		{
			notNullCheck.Failed(param1MissingError);
		}

		if (param2 is null)
		{
			notNullCheck.Failed(param2MissingError);
		}

		if (notNullCheck.HasFailed)
		{
			_validationResult.InheritFailure(notNullCheck);
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
		CanFail notNullCheck = new();
		if (param1 is null)
		{
			notNullCheck.Failed(param1MissingError);
		}

		if (param2 is null)
		{
			notNullCheck.Failed(param2MissingError);
		}

		if (notNullCheck.HasFailed)
		{
			_validationResult.InheritFailure(notNullCheck);
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
		CanFail notNullCheck = new();
		if (param1 is null)
		{
			notNullCheck.Failed(param1MissingError);
		}

		if (param2 is null)
		{
			notNullCheck.Failed(param2MissingError);
		}

		if (notNullCheck.HasFailed)
		{
			_validationResult.InheritFailure(notNullCheck);
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
		CanFail notNullCheck = new();
		if (param1 is null)
		{
			notNullCheck.Failed(param1MissingError);
		}

		if (param2 is null)
		{
			notNullCheck.Failed(param2MissingError);
		}

		if (notNullCheck.HasFailed)
		{
			_validationResult.InheritFailure(notNullCheck);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1!, param2!);
		if (result.HasFailed)
		{
			_validationResult.InheritFailure(result);
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
		CanFail notNullCheck = new();
		if (param1 is null)
		{
			notNullCheck.Failed(param1MissingError);
		}

		if (param2 is null)
		{
			notNullCheck.Failed(param2MissingError);
		}

		if (notNullCheck.HasFailed)
		{
			_validationResult.InheritFailure(notNullCheck);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1!.Value, param2!);
		if (result.HasFailed)
		{
			_validationResult.InheritFailure(result);
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
		CanFail notNullCheck = new();
		if (param1 is null)
		{
			notNullCheck.Failed(param1MissingError);
		}

		if (param2 is null)
		{
			notNullCheck.Failed(param2MissingError);
		}

		if (notNullCheck.HasFailed)
		{
			_validationResult.InheritFailure(notNullCheck);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1!, param2!.Value);
		if (result.HasFailed)
		{
			_validationResult.InheritFailure(result);
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
		CanFail notNullCheck = new();
		if (param1 is null)
		{
			notNullCheck.Failed(param1MissingError);
		}

		if (param2 is null)
		{
			notNullCheck.Failed(param2MissingError);
		}

		if (notNullCheck.HasFailed)
		{
			_validationResult.InheritFailure(notNullCheck);
			return default!;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1!.Value, param2!.Value);
		if (result.HasFailed)
		{
			_validationResult.InheritFailure(result);
			return default!;
		}

		return result.Value;
	}
}
