using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using Shouldly;

namespace Tests.ApplicationTests.PropertyBuilder;

public class ClassTests
{
    [Fact]
    public void ClassProperty_ShouldReturnClassProperty_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        var builder = propertyBuilder.ClassProperty(x => x.RequiredClassProperty);

        //Assert
        builder.ShouldBeOfType<ClassProperty<Parameters, string>>();
    }

    [Fact]
    public void ClassProperty_ShouldAddPropertyToList_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        var builder = propertyBuilder.ClassProperty(x => x.RequiredClassProperty);

        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }

    [Fact]
    public void ClassProperty_ShouldPushPropertyNameToNameStack_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        _ = propertyBuilder.ClassProperty(x => x.RequiredClassProperty);

        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredClassProperty"));
    }
    
    [Fact]
    public void ClassProperty_ShouldPushPropertyNameToNameStack_OnCastedClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        _ = propertyBuilder.ClassProperty(x => (string) x.RequiredClassProperty);

        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredClassProperty"));
    }
    
    [Fact]
    public void ClassProperty_ShouldThrowInvalidOperationException_OnSelfReferencingClassProperty()
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
    public void ClassProperty_ShouldThrowInvalidOperationException_OnCastedSelfReferencingClassProperty()
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
    public void ClassProperty_ShouldPushCustomNameToNameStack_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        _ = propertyBuilder.ClassProperty(x => x.RequiredClassProperty, Helpers.PropertyName);

        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void ClassProperty_ShouldPushCustomNameToNameStack_OnManualTypedClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        _ = propertyBuilder.ClassProperty<string>(Helpers.PropertyName);

        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
}