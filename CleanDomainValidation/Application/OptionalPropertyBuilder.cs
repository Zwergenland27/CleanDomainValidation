using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class OptionalPropertyBuilder<TParameters, TResult> : PropertyBuilder<TParameters, TResult>
	where TResult : notnull
{
	internal OptionalPropertyBuilder(TParameters parameters) : base(parameters) { }

	public ValidatedOptionalProperty<TResult> Build(Func<TResult> creationMethod)
	{
		CanFail<TResult?> result = new();

		bool requiredPropertyMissing = false;

		foreach (var property in Properties)
		{
			if(property.IsRequired && property.IsMissing)
			{
				requiredPropertyMissing = true;
				continue;
			}
			result.InheritFailure(property.ValidationResult);
		}

		if(!result.HasFailed && requiredPropertyMissing)
		{
			result.Succeeded(default);
		}

		if (!result.HasFailed && !requiredPropertyMissing)
		{
			result.Succeeded(creationMethod.Invoke());
		}

		return new ValidatedOptionalProperty<TResult>(result);
	}

	public ValidatedOptionalProperty<TResult> Build(Func<CanFail<TResult>> factoryMethod)
	{
		CanFail<TResult?> result = new();

		bool requiredPropertyMissing = false;

		foreach (var property in Properties)
		{
			if (property.IsRequired && property.IsMissing)
			{
				requiredPropertyMissing = true;
				continue;
			}
			result.InheritFailure(property.ValidationResult);
		}

		//Ensure the factory method wont get called if any errors occured to the parameters
		if (result.HasFailed)
		{
			return new ValidatedOptionalProperty<TResult>(result);
		}

		if (requiredPropertyMissing)
		{
			result.Succeeded(default);
			return new ValidatedOptionalProperty<TResult>(result);
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

		return new ValidatedOptionalProperty<TResult>(result);
	}
}
