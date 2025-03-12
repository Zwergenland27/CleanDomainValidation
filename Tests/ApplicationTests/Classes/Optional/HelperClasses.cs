using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.ApplicationTests.Classes.Optional;

public record OClassParameter(string? Value) : IParameters;

public record OClassValueObject(string Value)
{
    public static CanFail<OClassValueObject> Create(string value)
    {
        if (value == Helpers.ErrorStringValue) return Helpers.ExampleValidationError;
        return new OClassValueObject(value);
    }
}

public record OStructParameter(int? Value) : IParameters;

public record OStructValueObject(int Value)
{
    public static CanFail<OStructValueObject> Create(int value)
    {
        if (value == Helpers.ErrorIntValue) return Helpers.ExampleValidationError;
        return new OStructValueObject(value);
    }
}