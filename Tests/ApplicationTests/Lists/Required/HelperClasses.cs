using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;

namespace Tests.ApplicationTests.Lists.Required;

public record RClassListParameter(List<string>? Value) : IParameters;

public record RClassValueObject(string Value)
{
	public static CanFail<RClassValueObject> Create(string value)
	{
		if (value == Helpers.ErrorStringValue) return Helpers.ExampleValidationError;
		return new RClassValueObject(value);
	}
}

public record RStructListParameter(List<int>? Value) : IParameters;

public record RStructValueObject(int Value)
{
	public static CanFail<RStructValueObject> Create(int value)
	{
		if (value == Helpers.ErrorIntValue) return Helpers.ExampleValidationError;
		return new RStructValueObject(value);
	}
}
public record RStringListParameter(List<string>? Value) : IParameters;

public record RIntListParameter(List<int>? Value) : IParameters;
