using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Web.BLL.RequestHandlers
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        
        public BaseRequestHandler(IHttpContextAccessor httpContext, IMapper mapper)
        {
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        protected TDest Map<TDest, TSource>(TSource source)
        {
            return _mapper.Map<TDest>(source);
        }
        
        protected TDest Map<TDest>(object source)
        {
            return _mapper.Map<TDest>(source);
        }
    }
}