using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class RequiredPropertyBuilder<TParameters, TResult> : PropertyBuilder<TParameters, TResult>
	where TResult : notnull
{
	internal RequiredPropertyBuilder(TParameters parameters) : base(parameters) { }

	public ValidatedRequiredProperty<TResult> Build(Func<TResult> creationMethod)
	{
		CanFail<TResult> result = new();
		
		foreach(var property in Properties)
		{
			result.InheritFailure(property.ValidationResult);
		}

		if (!result.HasFailed)
		{
			result.Succeeded(creationMethod.Invoke());
		}

		return new ValidatedRequiredProperty<TResult>(result);
	}

	public ValidatedRequiredProperty<TResult> Build(Func<CanFail<TResult>> factoryMethod)
	{
		CanFail<TResult> result = new();

		foreach (var property in Properties)
		{
			result.InheritFailure(property.ValidationResult);
		}

		//Ensure the factory method wont get called if any errors occured to the parameters
		if (result.HasFailed)
		{
			return new ValidatedRequiredProperty<TResult>(result);
		}

		CanFail<TResult> creationResult = factoryMethod.Invoke();
		return new ValidatedRequiredProperty<TResult>(creationResult);
	}
}
