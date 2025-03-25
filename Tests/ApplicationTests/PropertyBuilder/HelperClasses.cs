using CleanDomainValidation.Application;

namespace Tests.ApplicationTests.PropertyBuilder;

public record Parameters() : IParameters;
public record Result(
    string RequiredClassProperty,
    int RequiredStructProperty,
    int? OptionalStructProperty,
    TestEnum RequiredEnumProperty,
    TestEnum? OptionalEnumProperty,
    List<int> RequiredListProperty);

public class TestablePropertyBuilder(Parameters parameters, NameStack nameStack) : PropertyBuilder<Parameters, Result>(parameters, nameStack)
{
    public new IReadOnlyList<ValidatableProperty> Properties => base.Properties;
}