using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using Shouldly;

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
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithValue_WhenNoParametersMissing()
    {
        //Arrange
        var parameters = new OSPParameter("value");
        var nameStack = new NameStack("");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OSPResult(value)).Build();

        //Assert
        result.Value.ShouldBe(new OSPResult("value"));
    }

    [Fact]
    public void MethodBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenParametersMissing()
    {
        //Arrange
        var parameters = new OSPParameter(null);
        var nameStack = new NameStack("");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => new OSPResult(value)).Build();

        //Assert
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(MissingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithValue_WhenNoParametersMissing()
    {
        //Arrange
        var parameters = new OSPParameter("value");
        var nameStack = new NameStack("");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OSPResult.Create(value)).Build();

        //Assert
        result.Value.ShouldBe(new OSPResult("value"));
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenParametersMissing()
    {
        //Arrange
        var parameters = new OSPParameter(null);
        var nameStack = new NameStack("");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OSPResult.Create(value)).Build();

        //Assert
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(MissingError);
    }

    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithValue_WhenNoBuildErrorsOccured()
    {
        //Arrange
        var parameters = new OSPParameter("value");
        var nameStack = new NameStack("");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OSPResult.Create(value)).Build();

        //Assert
        result.Value.ShouldBe(new OSPResult("value"));
    }
    
    [Fact]
    public void FactoryBuild_ShouldReturnValidatedRequiredPropertyWithErrors_WhenBuildErrorsOccured()
    {
        //Arrange
        var parameters = new OSPParameter("error");
        var nameStack = new NameStack("");
        var builder = new OptionalStructPropertyBuilder<OSPParameter, OSPResult>(parameters, nameStack);
        var value = builder.ClassProperty(x => x.Value)
            .Required(MissingError)
            .Map(x => x.Value);

        //Act
        var result = builder.Build(() => OSPResult.Create(value)).Build();

        //Assert
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(ExampleError);
    }
}
