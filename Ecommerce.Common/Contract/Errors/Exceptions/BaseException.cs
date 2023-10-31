
namespace Ecommerce.Common.Contract.Errors.Exceptions
{
    public class BaseException : Exception
    {
        public ErrorDetail[] Errors { get; private set; }

        public BaseException(string errorCode, string message = null, string[] localizationParams = null)
        {
            Errors = new[]
            {
                new ErrorDetail
                {
                    ErrorCode = errorCode,
                    ErrorMessage = message,
                }
            };
        }
        public BaseException(ErrorDetail error)
        {
            Errors = new[]
            {
                error
            };
        } // BaseException

        public BaseException(ErrorDetail[] errors)
        {
            Errors = errors;
        } // BaseException

        public BaseException(BaseError error)
        {
            Errors = error.Errors;
        } // BaseException
    }

}
