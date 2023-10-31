using System;
using Ecommerce.Common.Contract.Errors.Exceptions;
using Microsoft.AspNetCore.Http;


namespace Ecommerce.Common.Contract.Errors
{
    public class ErrorsFactory: IErrorsFactory
    {
        public ErrorsFactory() { }

        public BaseError CreateBaseError(HttpContext httpContext, Exception ex, int statusCode)
        {
            BaseError error;
            if (ex is ValidationException validationException)
            {
                error = CreateValidationError(httpContext, validationException, statusCode);
            } else if (ex is ForbiddenException forbiddenException)
            {
                error = CreateForbiddenError(httpContext, forbiddenException, StatusCodes.Status403Forbidden);
            } else if (ex is FunctionDisabledException functionDisabledException)
            {
                error = CreateFunctionDisabledException(httpContext, functionDisabledException, statusCode);
            } else if (ex is UserException userException)
            {
                error = CreateUserError(httpContext, userException, statusCode);
            } else if (ex is BaseException baseException)
            {
                error = CreateBaseError(httpContext, baseException, statusCode);
            } 
            else
            {
                error = CreateUnhandledError(httpContext, ex, statusCode);
            } // if

            return error;
        } // CreateBaseError

        private static BaseError CreateValidationError(HttpContext httpContext, ValidationException ex, int statusCode)
        {
            return new BaseError()
            {
                Title = "One or more vaidation errors occured.",
                Status = statusCode,
                TraceId = httpContext.TraceIdentifier,
                Type = ex.HelpLink,
                Errors = ex.Errors
            };
        } // CreateValidationError

        private static BaseError CreateFunctionDisabledException(HttpContext httpContext, FunctionDisabledException ex,
            int statusCode)
        {
            return new BaseError()
            {
                Title = "Function disabled.",
                Status = statusCode,
                TraceId = httpContext.TraceIdentifier,
                Type = ex.HelpLink,
                Errors = ex.Errors
            };
        } // CreateFunctionDisabledException

        private static BaseError CreateForbiddenError(HttpContext httpContext, ForbiddenException ex, int statusCode)
        {
            return new BaseError()
            {
                Title = "Access denied.",
                Status = statusCode,
                TraceId = httpContext.TraceIdentifier,
                Type = ex.HelpLink,
                Errors = ex.Errors
            };
        }
        private static BaseError CreateBaseError(HttpContext httpContext, BaseException ex, int statusCode)
        {
            return new BaseError()
            {
                Title = "One or more errors occured.",
                Status = statusCode,
                TraceId = httpContext.TraceIdentifier,
                StackTrace = ex.StackTrace,
                Type = ex.HelpLink,
                Errors = ex.Errors
            };
        } // CreateBaseError

        private static BaseError CreateUnhandledError(HttpContext httpContext, Exception ex, int statusCode)
        {
            return new BaseError()
            {
                Title = "One or more unhandled errors occured.",
                Status = statusCode,
                TraceId = httpContext.TraceIdentifier,
                StackTrace = ex.StackTrace,
                Type = ex.HelpLink,
                Errors = new[]
                {
                    new ErrorDetail
                    {
                        ErrorCode = ErrorCodes.AppErrors.UnhandledApplicationError,
                        ErrorMessage = ex.InnerException?.Message ?? ex.Message,
                    }
                }
            };
        } // CreateUnhandledError
        
        private static BaseError CreateUserError(HttpContext httpContext, UserException ex, int statusCode)
        {
            return new BaseError()
            {
                Title = "Something goes wrong.",
                Status = statusCode,
                TraceId = httpContext.TraceIdentifier,
                Type = ex.HelpLink,
                Errors = ex.Errors
            };
        } // CreateUnhandledError
    }
}
