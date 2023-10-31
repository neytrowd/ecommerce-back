using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Common.Contract.Errors
{
    public class BaseError: ProblemDetails
    {
        public string TraceId { get; set; }
        public string StackTrace { get; set; }
        public ErrorDetail[] Errors { get; set; }
        public BaseError() {}
    }
}
