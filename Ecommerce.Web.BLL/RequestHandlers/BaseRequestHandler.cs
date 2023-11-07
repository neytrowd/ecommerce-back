using AutoMapper;
using Ecommerce.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Web.BLL.RequestHandlers
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private long? _applicationUserId;
        
        private readonly IMapper _mapper;
        
        protected readonly IHttpContextAccessor _httpContext;
        
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
        
        protected long ApplicationUserId
        {
            get
            {
                if (_applicationUserId.HasValue)
                {
                    return _applicationUserId.Value;
                }

                var applicationUserId = _httpContext.HttpContext.GetApplicationUserId();
                _applicationUserId = applicationUserId;

                return applicationUserId;
            }
        }
    }
}