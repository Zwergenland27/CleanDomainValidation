// See https://aka.ms/new-console-template for more information
using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;

Params request = new(null);

var command = CommandBuilder<Rekord>
	.AddParameter(request)
	.ValidateByUsing<Tests>();

if (command.HasFailed)
{
	Console.Write(command.Errors[0].Code);
	return;
}

Console.WriteLine(command.Value);

internal class Tests : CommandValidator<Params, Rekord>
{
	public override void Configure(Params parameter)
	{
		IEnumerable<ValueObject>? t = OptionalList(
			parameter.List,
			parameter => ValueObject.Create(parameter));

		CreateInstance(() => new Rekord(t?.ToList()));
	}
}

public record Params(List<string>? List) : IParameter;

public record Rekord(List<ValueObject>? list) : ICommand;


public record ValueObject(string Value)
{
	public static CanFail<ValueObject> Create(string Value)
	{
		if (Value == "Störung") return Error.Conflict("Fehöler", "");
		return new ValueObject(Value);
	}
}