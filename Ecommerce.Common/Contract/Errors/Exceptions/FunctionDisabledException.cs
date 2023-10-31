namespace Ecommerce.Common.Contract.Errors.Exceptions;

public class FunctionDisabledException : BaseException
{
    public FunctionDisabledException(string errorCode, string message = null)
        : base(errorCode, message)
    {
    }
}