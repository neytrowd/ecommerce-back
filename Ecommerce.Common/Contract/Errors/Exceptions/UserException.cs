namespace Ecommerce.Common.Contract.Errors.Exceptions;

public class UserException: BaseException
{
    public UserException(string errorCode, string message = null) : base(errorCode, message)
    {
    }
    
}