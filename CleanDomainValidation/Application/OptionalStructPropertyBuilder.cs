using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class OptionalStructPropertyBuilder<TParameters, TResult> : PropertyBuilder<TParameters, TResult>
	where TParameters : notnull
	where TResult : struct
{
	internal OptionalStructPropertyBuilder(TParameters parameters) : base(parameters) { }

	public ValidatedOptionalStructProperty<TResult> Build(Func<TResult> creationMethod)
	{
		CanFail<TResult?> result = new();


		foreach (var property in Properties)
		{
			result.InheritFailure(property.ValidationResult);
		}

		if (!result.HasFailed)
		{
			result.Succeeded(creationMethod.Invoke());
		}

		return new ValidatedOptionalStructProperty<TResult>(result);
	}

	public ValidatedOptionalStructProperty<TResult> Build(Func<CanFail<TResult>> factoryMethod)
	{
		CanFail<TResult?> result = new();

		foreach (var property in Properties)
		{
			result.InheritFailure(property.ValidationResult);
		}

		//Ensure the factory method wont get called if any errors occured to the parameters
		if (result.HasFailed)
		{
			return new ValidatedOptionalStructProperty<TResult>(result);
		}

		CanFail<TResult> creationResult = factoryMethod.Invoke();
		if (creationResult.HasFailed)
		{
			result.InheritFailure(creationResult);
		}
		else
		{
			result.Succeeded(creationResult.Value);
		}

		return new ValidatedOptionalStructProperty<TResult>(result);
	}
}
