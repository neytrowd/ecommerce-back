namespace Ecommerce.Common.Contract.Errors.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(ErrorDetail[] errors) : base(errors) { }

        public ValidationException(string errorCode, string message = null) 
            : base(errorCode, message) { }
    }
}
