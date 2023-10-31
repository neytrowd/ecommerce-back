namespace Ecommerce.Common.Contract.Errors
{
    public static class ErrorCodes
    {
        public static class AppErrors
        {
            public static readonly string UnhandledApplicationError = "UnhandledApplicationError";
            public static readonly string ValidationError = "ValidationError";
            public static readonly string ForbiddenError = "ForbiddenError";
        }
        
        public static class AppAuth
        {
            public static readonly string UserNotFoundError = "UserNotFoundError";
            public static readonly string IncorrectPasswordError = "IncorrectPasswordError";
            public static readonly string RefreshTokenNotExistsError = "RefreshTokenNotExistsError";
            public static readonly string RefreshTokenIsExpiredError = "RefreshTokenIsExpiredError";
            public static readonly string InvalidEmailTokenError = "InvalidEmailTokenError";
            public static readonly string WrongPasswordError = "WrongPasswordError";
        }

        public static class AppUser
        {
            public static readonly string UserWithTheSameLoginAlreadyExistsError = "UserWithTheSameLoginAlreadyExistsError";
            public static readonly string AccessToResourceDenied = "AccessToResourceDenied";
        }
    }
}
