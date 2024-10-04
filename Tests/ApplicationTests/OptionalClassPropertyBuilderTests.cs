using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests;

public record OCPParameter(string? Value) : IParameters;

public record OCPResult(string? Value)
{
    public static CanFail<OCPResult> Create(string value)
    {
        if (value == "error")
        {
            return RequiredPropertyBuilderTests.ExampleError;
        }

        return new OCPResult(value);
    }
}
public class OptionalClassPropertyBuilderTests
{
    public static Error ExampleError => Error.Validation("Validation.Error", "An error occured");

    private static Error MissingError => Error.Validation("Error.Missing", "Value is missing");

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithoutErrors_WhenNoErrorsOccured()
    {
        //Arrange
        var parameters = new OCPParameter("value");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OCPResult(value)).Build();

        //Assert
        result.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithResult_WhenNoPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new OCPParameter("value");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OCPResult(value)).Build();

        //Assert
        result.Value.Should().Be(new OCPResult("value"));
    }

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new OCPParameter(null);
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OCPResult(value)).Build();

        //Assert
        result.Errors.Should().Contain(MissingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithoutErrors_WhenNoErrorsOccured()
    {
        //Arrange
        var parameters = new OCPParameter("value");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OCPResult.Create(value)).Build();

        //Assert
        result.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithResult_WhenNoPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new OCPParameter("value");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OCPResult.Create(value)).Build();

        //Assert
        result.Value.Should().Be(new OCPResult("value"));
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new OCPParameter(null);
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OCPResult.Create(value)).Build();

        //Assert
        result.Errors.Should().Contain(MissingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenBuildErrorsOccured()
    {
        //Arrange
        var parameters = new OCPParameter("error");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OCPResult.Create(value)).Build();

        //Assert
        result.Errors.Should().Contain(ExampleError);
    }
}
