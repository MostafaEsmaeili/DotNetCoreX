using DotNetCore.Objects;
using Newtonsoft.Json;

namespace Framework.Objects
{
    public sealed class CustomSuccessResult : Result
    {
        [JsonConstructor]

        public CustomSuccessResult(string message)
            : base(true, message)
        {
        }
        public CustomSuccessResult()
            : base(true)
        {
        }
    }
}