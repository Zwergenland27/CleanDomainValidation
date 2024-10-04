using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests;

public record OSPParameter(string? Value) : IParameters;

public record struct OSPResult(string? Value)
{
    public static CanFail<OSPResult> Create(string value)
    {
        if (value == "error")
        {
            return RequiredPropertyBuilderTests.ExampleError;
        }

        return new OSPResult(value);
    }
}

public class OptionalStructPropertyBuilderTests
{
    public static Error ExampleError => Error.Validation("Validation.Error", "An error occured");

    private static Error MissingError => Error.Validation("Error.Missing", "Value is missing");

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithoutErrors_WhenNoErrorsOccured()
    {
        //Arrange
        var parameters = new OSPParameter("value");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OSPResult(value)).Build();

        //Assert
        result.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithResult_WhenNoPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new OSPParameter("value");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OSPResult(value)).Build();

        //Assert
        result.Value.Should().Be(new OSPResult("value"));
    }

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new OSPParameter(null);
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OSPResult(value)).Build();

        //Assert
        result.Errors.Should().Contain(MissingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithoutErrors_WhenNoErrorsOccured()
    {
        //Arrange
        var parameters = new OSPParameter("value");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OSPResult.Create(value)).Build();

        //Assert
        result.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithResult_WhenNoPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new OSPParameter("value");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OSPResult.Create(value)).Build();

        //Assert
        result.Value.Should().Be(new OSPResult("value"));
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new OSPParameter(null);
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OSPResult.Create(value)).Build();

        //Assert
        result.Errors.Should().Contain(MissingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenBuildErrorsOccured()
    {
        //Arrange
        var parameters = new OSPParameter("error");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OSPResult.Create(value)).Build();

        //Assert
        result.Errors.Should().Contain(ExampleError);
    }
}
