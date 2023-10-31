using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class BaseController: Controller
    {
        private long? _applicationUserId;

        public BaseController()
        {
            
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