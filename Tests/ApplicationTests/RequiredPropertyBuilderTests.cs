using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using FluentAssertions;
using System.Xml.Schema;

namespace Tests.ApplicationTests;

public record RPParameter(string? Value) : IParameters;

public record RPResult(string? Value)
{
    public static CanFail<RPResult> Create(string value)
    {
        if(value == "error")
        {
            return RequiredPropertyBuilderTests.ExampleError;
        }

        return new RPResult(value);
    }
}

public class RequiredPropertyBuilderTests
{
    public static Error ExampleError => Error.Validation("Validation.Error", "An error occured");

    private static Error _missingError => Error.Validation("Error.Missing", "Value is missing");

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithoutErrors_WhenNoErrorsOccured()
    {
        //Arrange
        var parameters = new RPParameter("value");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(_missingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new RPResult(value)).Build();

        //Assert
        result.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithResult_WhenNoPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new RPParameter("value");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(_missingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new RPResult(value)).Build();

        //Assert
        result.Value.Should().Be(new RPResult("value"));
    }

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new RPParameter(null);
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(_missingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new RPResult(value)).Build();

        //Assert
        result.Errors.Should().Contain(_missingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithoutErrors_WhenNoErrorsOccured()
    {
        //Arrange
        var parameters = new RPParameter("value");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(_missingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => RPResult.Create(value)).Build();

        //Assert
        result.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithResult_WhenNoPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new RPParameter("value");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(_missingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => RPResult.Create(value)).Build();

        //Assert
        result.Value.Should().Be(new RPResult("value"));
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenPropertyErrorsOccured()
    {
        //Arrange
        var parameters = new RPParameter(null);
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(_missingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => RPResult.Create(value)).Build();

        //Assert
        result.Errors.Should().Contain(_missingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenBuildErrorsOccured()
    {
        //Arrange
        var parameters = new RPParameter("error");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters);
        var value = builder.ClassProperty(x => x.Value)
            .Required(_missingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => RPResult.Create(value)).Build();

        //Assert
        result.Errors.Should().Contain(ExampleError);
    }
}
