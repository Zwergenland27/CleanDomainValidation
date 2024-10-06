using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests;

public class ValidatedRequiredPropertyTests
{
    [Fact]
    public void Build_ShouldReturnResultFromConstructor()
    {
        //Arrange
        var result = new CanFail<string>();
        var property = new ValidatedRequiredProperty<string>(result);

        //Act
        var returnResult = property.Build();

        //Assert
        returnResult.Should().Be(result);
    }
}