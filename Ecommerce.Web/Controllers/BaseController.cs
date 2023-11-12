using AutoMapper;
using Ecommerce.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [ApiController]
    [Authorize]
    public abstract class BaseController: Controller
    {
        private long? _applicationUserId;

        private readonly IMapper _mapper;

        protected readonly IMediator _mediator;

        public BaseController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

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

                var applicationUserId = HttpContext.GetApplicationUserId();
                _applicationUserId = applicationUserId;

                return applicationUserId;
            }
        }
    }
}