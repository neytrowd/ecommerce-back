using MediatR;

namespace Ecommerce.Common.Contract
{
    public class BaseRequest<TResponse> : IRequest<TResponse>
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
    }
}