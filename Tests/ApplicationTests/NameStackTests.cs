using System.Runtime.CompilerServices;
using CleanDomainValidation.Application;
using Shouldly;

namespace Tests.ApplicationTests;

public class NameStackTests
{
    [Fact]
    public void PushProperty_ShouldPushPropertyNameToStack()
    {
        //Arrange
        var nameStack = new NameStack("");
        
        //Act
        nameStack.PushProperty(Helpers.PropertyName);
        
        //Assert
        nameStack.ShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    public static IEnumerable<object[]> PopPropertyTopElementTestData = new List<object[]>()
    {
        new object[] { new[] { "One" }, new PropertyNameEntry("One") },
        new object[] { new[] { "One", "Two" }, new PropertyNameEntry("Two") },
    };
    
    [Theory, MemberData(nameof(PopPropertyTopElementTestData))]
    public void PopProperty_ShouldReturnAndRemoveTopElement(string[] propertyNames, INameStackEntry poppedElement)
    {
        //Arrange
        var nameStack = new NameStack("");
        foreach (var propertyName in propertyNames)
        {
            nameStack.PushProperty(propertyName);
        }
        
        //Act
        var element = nameStack.Pop();
        
        //Assert
        element.ShouldBe(poppedElement);
        nameStack.ShouldNotPeek(poppedElement);
    }

    [Fact]
    public void MissingErrorCode_ShouldThrow_WhenStackIsEmpty()
    {
        //Arrange
        var nameStack = new NameStack("");
        
        //Assert
        Should.Throw<InvalidOperationException>(() => nameStack.MissingErrorCode);
    }

    [Theory]
    [InlineData("", new [] {"MainProperty"}, "MainProperty.Missing")]
    [InlineData("", new [] {"MainProperty", "SecondaryProperty"}, "MainProperty.SecondaryProperty.Missing")]
    [InlineData("Prefix", new [] {"MainProperty"}, "Prefix.MainProperty.Missing")]
    [InlineData("Prefix", new [] {"MainProperty", "SecondaryProperty"}, "Prefix.MainProperty.SecondaryProperty.Missing")]
    [InlineData("Prefix.Method", new [] {"MainProperty"}, "Prefix.Method.MainProperty.Missing")]
    [InlineData("Prefix.Method", new [] {"MainProperty", "SecondaryProperty"}, "Prefix.Method.MainProperty.SecondaryProperty.Missing")]
    public void MissingErrorCode_ShouldBeGeneratedBasedOnStack(string prefix, string[] propertyNames, string expectedErrorCode)
    {
        //Arrange
        var nameStack = new NameStack(prefix);
        
        //Act
        foreach (var propertyName in propertyNames)
        {
            nameStack.PushProperty(propertyName);
        }
        
        //Assert
        nameStack.MissingErrorCode.ShouldBe(expectedErrorCode);
    }
    
    [Fact]
    public void MissingErrorMessage_ShouldThrow_WhenStackIsEmpty()
    {
        //Arrange
        var nameStack = new NameStack("");
        
        //Assert
        Should.Throw<InvalidOperationException>(() => nameStack.MissingErrorMessage);
    }
    
    [Theory]
    [InlineData("", new [] {"MainProperty"}, "Property \"MainProperty\" is required but missing or null in the request.")]
    [InlineData("", new [] {"MainProperty", "SecondaryProperty"}, "Property \"SecondaryProperty\" is required but missing or null in the request.")]
    [InlineData("Prefix", new [] {"MainProperty"}, "Property \"MainProperty\" is required but missing or null in the request.")]
    [InlineData("Prefix", new [] {"MainProperty", "SecondaryProperty"}, "Property \"SecondaryProperty\" is required but missing or null in the request.")]
    [InlineData("Prefix.Method", new [] {"MainProperty"}, "Property \"MainProperty\" is required but missing or null in the request.")]
    [InlineData("Prefix.Method", new [] {"MainProperty", "SecondaryProperty"}, "Property \"SecondaryProperty\" is required but missing or null in the request.")]
    public void MissingErrorMessage_ShouldBeGeneratedBasedOnStack(string prefix, string[] propertyNames, string expectedErrorMessage)
    {
        //Arrange
        var nameStack = new NameStack(prefix);
        
        //Act
        foreach (var propertyName in propertyNames)
        {
            nameStack.PushProperty(propertyName);
        }
        
        //Assert
        nameStack.MissingErrorMessage.ShouldBe(expectedErrorMessage);
    }
}