using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.ApplicationTests;

public class ValidatedOptionalStructPropertyTests
{
    [Fact]
    public void Build_ShouldReturnResultFromConstructor()
    {
        //Arrange
        var result = new CanFail<int?>();
        var property = new ValidatedOptionalStructProperty<int>(result);

        //Act
        var returnResult = property.Build();

        //Assert
        returnResult.ShouldBe(result);
    }
}