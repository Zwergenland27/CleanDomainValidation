namespace CleanDomainValidation.Application;

public interface IRequestBuilder<TParameter, TRequest>
{
	Configured<TRequest> Configure(RequestBuilder<TParameter, TRequest> builder);
}
