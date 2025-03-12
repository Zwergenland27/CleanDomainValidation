namespace CleanDomainValidation.Application;

/// <summary>
/// Stack that contains the name of all nested properties
/// </summary>
public class NameStack
{
    private readonly Stack<INameStackEntry> _propertyNamesStack;
    private readonly string _prefix;
    
    internal NameStack(string prefix)
    {
        _prefix = prefix;
        _propertyNamesStack = [];
    }

    /// <summary>
    /// Missing error code based on the current name stack
    /// </summary>
    /// <example>Users.Get.Username.Missing</example>
    public string MissingErrorCode => GenerateErrorCode();

    /// <summary>
    /// Missing error message
    /// </summary>
    /// <example>Property Username is required but missing or null in the request.</example>
    public string MissingErrorMessage => GenerateErrorMessage();

    /// <summary>
    /// Push property with name <paramref name="name"/> on the stack
    /// </summary>
    internal void PushProperty(string name)
    {
        _propertyNamesStack.Push(new PropertyNameEntry(name));
    }

    /// <summary>
    /// Get and remove stack's top element
    /// </summary>
    /// <returns>Top element</returns>
    internal INameStackEntry Pop()
    {
        return _propertyNamesStack.Pop();
    }

    private string GenerateErrorCode()
    {
        var result = _prefix;
        if (result.Length > 0) result += ".";
        return result + string.Join(".", _propertyNamesStack
            .Reverse()
            .OfType<PropertyNameEntry>()
            .Select(e => e.Name)) + ".Missing";
    }

    private string GenerateErrorMessage()
    {
        var lastPropertyName = (PropertyNameEntry)_propertyNamesStack.Peek();

        return$"Property \"{lastPropertyName.Name}\" is required but missing or null in the request.";
    }
}