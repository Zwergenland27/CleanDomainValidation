using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using Shouldly;

namespace Tests.ApplicationTests.PropertyBuilder;

public class EnumTests
{
    [Fact]
    public void EnumProperty_ShouldReturnEnumProperty_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty);
    
        //Assert
        builder.ShouldBeOfType<EnumProperty<Parameters, TestEnum>>();
    }
    
    [Fact]
    public void EnumProperty_ShouldAddPropertyToList_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty);
    
        //Assert
        propertyBuilder.Properties.ShouldContain(builder);
    }

    [Fact]
    public void EnumProperty_ShouldPushPropertyNameToNameStack_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
        
        //Act
        _ = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty);
        
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredEnumProperty"));
    }
    
    [Fact]
    public void EnumProperty_ShouldPushPropertyNameToNameStack_OnCastedRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
        
        //Act
        _ = propertyBuilder.EnumProperty(x => (TestEnum) x.RequiredEnumProperty);
        
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredEnumProperty"));
    }
    
    [Fact]
    public void EnumProperty_ShouldThrowInvalidOperationException_OnSelfReferencingRequiredEnumProperty()
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
    public void EnumProperty_ShouldThrowInvalidOperationException_OnCastedSelfReferencingRequiredEnumProperty()
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
    public void EnumProperty_ShouldPushCustomNameToNameStack_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
        
        //Act
        _ = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty, Helpers.PropertyName);
        
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void EnumProperty_ShouldReturnEnumProperty_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty);
    
        //Assert
        builder.ShouldBeOfType<EnumProperty<Parameters, TestEnum>>();
    }
    
    [Fact]
    public void EnumProperty_ShouldAddPropertyToList_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty);
    
        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }
    
    [Fact]
    public void EnumProperty_ShouldPushPropertyNameToNameStack_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("OptionalEnumProperty"));
    }
    
    [Fact]
    public void EnumProperty_ShouldPushPropertyNameToNameStack_OnCastedOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.EnumProperty(x => (TestEnum?) x.OptionalEnumProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("OptionalEnumProperty"));
    }
    
    [Fact]
    public void EnumProperty_ShouldThrowInvalidOperationException_OnSelfReferencingOptionalEnumProperty()
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
    public void EnumProperty_ShouldThrowInvalidOperationException_OnCastedSelfReferencingOptionalEnumProperty()
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
    public void EnumProperty_ShouldPushCustomNameToNameStack_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty, Helpers.PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void EnumProperty_ShouldPushCustomNameToNameStack_OnManualTypedEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
        
        //Act
        _ = propertyBuilder.EnumProperty<TestEnum>(Helpers.PropertyName);
        
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
}