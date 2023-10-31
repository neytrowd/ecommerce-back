
namespace Ecommerce.Common.Contract.Errors.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string errorCode, string message = null) : base(errorCode, message)
        {
        }
    }
}
