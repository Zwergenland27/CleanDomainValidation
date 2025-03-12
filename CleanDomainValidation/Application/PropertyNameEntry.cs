namespace CleanDomainValidation.Application;

public interface INameStackEntry;
public record PropertyNameEntry(string Name) : INameStackEntry;