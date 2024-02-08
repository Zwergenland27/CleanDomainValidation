namespace CleanDomainValidation.Application;

public interface IRequestBuilder<TParameters, TRequest>
	where TParameters : IParameters
	where TRequest : IRequest

{
	Configured<TRequest> Configure(RequestBuilder<TParameters, TRequest> builder);
}
