using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Structs;
using Shouldly;

namespace Tests.ApplicationTests.PropertyBuilder;

public class StructTests
{
    [Fact]
    public void StructProperty_ShouldReturnStructProperty_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.StructProperty(x => x.RequiredStructProperty);
    
        //Assert
        builder.ShouldBeOfType<StructProperty<Parameters, int>>();
    }
    
    [Fact]
    public void StructProperty_ShouldAddPropertyToList_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.StructProperty(x => x.RequiredStructProperty);
    
        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }
    
    [Fact]
    public void StructProperty_ShouldPushPropertyNameToNameStack_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => x.RequiredStructProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredStructProperty"));
    }
    
    [Fact]
    public void StructProperty_ShouldPushPropertyNameToNameStack_OnCastedRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => (int) x.RequiredStructProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredStructProperty"));
    }
    
    [Fact]
    public void StructProperty_ShouldThrowInvalidOperationException_OnSelfReferencingRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act & Assert
        Should.Throw<InvalidOperationException>(() => propertyBuilder.ClassProperty(x => x))
            .Message.ShouldBe(Helpers.SelfReferencingErrorMessage);
    }
    
    [Fact]
    public void StructProperty_ShouldThrowInvalidOperationException_OnCastedSelfReferencingRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act & Assert
        Should.Throw<InvalidOperationException>(() => propertyBuilder.ClassProperty(x => (Result) x))
            .Message.ShouldBe(Helpers.SelfReferencingErrorMessage);
    }
    
    [Fact]
    public void StructProperty_ShouldPushCustomNameToNameStack_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => x.RequiredStructProperty, Helpers.PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void StructProperty_ShouldReturnStructProperty_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.StructProperty(x => x.OptionalStructProperty);
    
        //Assert
        builder.ShouldBeOfType<StructProperty<Parameters, int>>();
    }
    
    [Fact]
    public void StructProperty_ShouldAddPropertyToList_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.StructProperty(x => x.OptionalStructProperty);
    
        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }
    
    [Fact]
    public void StructProperty_ShouldPushPropertyNameToNameStack_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => x.OptionalStructProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("OptionalStructProperty"));
    }
    
    [Fact]
    public void StructProperty_ShouldPushPropertyNameToNameStack_OnCastedOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => (int?) x.OptionalStructProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("OptionalStructProperty"));
    }
    
    [Fact]
    public void StructProperty_ShouldThrowInvalidOperationException_OnSelfReferencingOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act & Assert
        Should.Throw<InvalidOperationException>(() => propertyBuilder.ClassProperty(x => x))
            .Message.ShouldBe(Helpers.SelfReferencingErrorMessage);
    }
    
    [Fact]
    public void StructProperty_ShouldThrowInvalidOperationException_OnCastedSelfReferencingOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act & Assert
        Should.Throw<InvalidOperationException>(() => propertyBuilder.ClassProperty(x => (Result) x))
            .Message.ShouldBe(Helpers.SelfReferencingErrorMessage);
    }
    
    [Fact]
    public void StructProperty_ShouldPushCustomNameToNameStack_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => x.OptionalStructProperty, Helpers.PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void StructProperty_ShouldPushCustomNameToNameStack_OnManualTypedStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty<int>(Helpers.PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
}