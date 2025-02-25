using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using Shouldly;

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
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithValue_WhenNoParametersMissing()
    {
        //Arrange
        var parameters = new OCPParameter("value");
        var nameStack = new NamingStack("");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OCPResult(value)).Build();

        //Assert
        result.Value.ShouldBe(new OCPResult("value"));
    }

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenParametersMissing()
    {
        //Arrange
        var parameters = new OCPParameter(null);
        var nameStack = new NamingStack("");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OCPResult(value)).Build();

        //Assert
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(MissingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithValue_WhenNoParametersMissing()
    {
        //Arrange
        var parameters = new OCPParameter("value");
        var nameStack = new NamingStack("");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OCPResult.Create(value)).Build();

        //Assert
        result.Value.ShouldBe(new OCPResult("value"));
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenParametersMissing()
    {
        //Arrange
        var parameters = new OCPParameter(null);
        var nameStack = new NamingStack("");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OCPResult.Create(value)).Build();

        //Assert
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(MissingError);
    }
    
    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithValue_WhenNoBuildErrorsOccured()
    {
        //Arrange
        var parameters = new OCPParameter("value");
        var nameStack = new NamingStack("");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OCPResult.Create(value)).Build();

        //Assert
        result.Value.ShouldBe(new OCPResult("value"));
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenBuildErrorsOccured()
    {
        //Arrange
        var parameters = new OCPParameter("error");
        var nameStack = new NamingStack("");
        var builder = new OptionalClassPropertyBuilder<OCPParameter, OCPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OCPResult.Create(value)).Build();

        //Assert
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(ExampleError);
    }
}
