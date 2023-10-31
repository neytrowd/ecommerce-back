using Ecommerce.Common.Contract.Errors;
using Newtonsoft.Json;

namespace Ecommerce.Common.Contract
{
    public class BaseResponse
    {
        [JsonProperty("errors")]
        public List<ErrorDetail> Errors { get; set; }
    }
}