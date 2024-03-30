using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class OptionalClassPropertyBuilder<TParameters, TResult> : PropertyBuilder<TParameters, TResult>
	where TParameters : notnull
	where TResult : class
{
	internal OptionalClassPropertyBuilder(TParameters parameters) : base(parameters) { }

	public ValidatedOptionalClassProperty<TResult> Build(Func<TResult> creationMethod)
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
			result.Succeeded(null);
		}

		if (!result.HasFailed && !requiredPropertyMissing)
		{
			result.Succeeded(creationMethod.Invoke());
		}

		return new ValidatedOptionalClassProperty<TResult>(result);
	}

	public ValidatedOptionalClassProperty<TResult> Build(Func<CanFail<TResult>> factoryMethod)
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
			return new ValidatedOptionalClassProperty<TResult>(result);
		}

		if (requiredPropertyMissing)
		{
			result.Succeeded(null);
			return new ValidatedOptionalClassProperty<TResult>(result);
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

		return new ValidatedOptionalClassProperty<TResult>(result);
	}
}
