using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using Shouldly;

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

    private static Error MissingError => Error.Validation("Error.Missing", "Value is missing");

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithValue_WhenNoParametersMissing()
    {
        //Arrange
        var parameters = new RPParameter("value");
        var nameStack = new NameStack("");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new RPResult(value)).Build();

        //Assert
        result.Value.ShouldBe(new RPResult("value"));
    }

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenParametersMissing()
    {
        //Arrange
        var parameters = new RPParameter(null);
        var nameStack = new NameStack("");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new RPResult(value)).Build();

        //Assert
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(MissingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithValue_WhenNoParametersMissing()
    {
        //Arrange
        var parameters = new RPParameter("value");
        var nameStack = new NameStack("");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => RPResult.Create(value)).Build();

        //Assert
        result.Value.ShouldBe(new RPResult("value"));
    }
    
    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenParametersMissing()
    {
        //Arrange
        var parameters = new RPParameter(null);
        var nameStack = new NameStack("");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => RPResult.Create(value)).Build();

        //Assert
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(MissingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithValue_WhenNoBuildErrorsOccured()
    {
        //Arrange
        var parameters = new RPParameter("value");
        var nameStack = new NameStack("");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => RPResult.Create(value)).Build();

        //Assert
        result.Value.ShouldBe(new RPResult("value"));
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenBuildErrorsOccured()
    {
        //Arrange
        var parameters = new RPParameter("error");
        var nameStack = new NameStack("");
        var builder = new RequiredPropertyBuilder<RPParameter, RPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => RPResult.Create(value)).Build();

        //Assert
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(ExampleError);
    }
}
