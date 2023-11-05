using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class BaseController: Controller
    {
        private long? _applicationUserId;

        private readonly IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
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

                _applicationUserId = 0;

                return _applicationUserId.Value;
            }
        }
    }
}