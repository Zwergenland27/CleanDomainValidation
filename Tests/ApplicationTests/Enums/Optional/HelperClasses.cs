using CleanDomainValidation.Application;

namespace Tests.ApplicationTests.Enums.Optional;

public record OStringParameter(string? Value) : IParameters;

public record OIntParameter(int? Value) : IParameters;
