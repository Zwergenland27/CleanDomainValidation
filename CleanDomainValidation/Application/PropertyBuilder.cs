using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public sealed class PropertyBuilder<TParameter, TProperty> : Builder<TParameter, TProperty>
{
	internal PropertyBuilder(TParameter parameters) : base(parameters) { }

	public ValidatedProperty<TProperty> Build(Func<TProperty> creationMethod)
	{
		CanFail result = new();

		Properties.ForEach(property =>
		{
			result.InheritFailure(property.ValidationResult);
		});

		if (result.HasFailed)
		{
			return new ValidatedProperty<TProperty>(default!, result);
		}

		return new ValidatedProperty<TProperty>(creationMethod.Invoke(), result);
	}

	public ValidatedProperty<TProperty> Build(Func<CanFail<TProperty>> factoryMethod)
	{
		CanFail result = new();

		Properties.ForEach(property =>
		{
			result.InheritFailure(property.ValidationResult);
		});

		//Ensure the factory method wont get called if any errors occured to the parameters
		if (result.HasFailed)
		{
			return new ValidatedProperty<TProperty>(default!, result);
		}

		var creationResult = factoryMethod.Invoke();
		result.InheritFailure(creationResult);

		if (result.HasFailed)
		{
			return new ValidatedProperty<TProperty>(default!, result);
		}

		return new ValidatedProperty<TProperty>(creationResult.Value, result);
	}
}
