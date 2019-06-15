using DotNetCore.Objects;
using Newtonsoft.Json;

namespace Framework.Objects
{
    public sealed class CustomErrorResult : Result
    {
        [JsonConstructor]

        public CustomErrorResult(string message)
            : base(false, message)
        {
        }
        public CustomErrorResult()
            : base(false)
        {
        }
    }
}