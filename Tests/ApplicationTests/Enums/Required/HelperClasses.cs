using CleanDomainValidation.Application;

namespace Tests.ApplicationTests.Enums.Required;

public record RStringParameter(string? Value) : IParameters;

public record RIntParameter(int? Value) : IParameters;