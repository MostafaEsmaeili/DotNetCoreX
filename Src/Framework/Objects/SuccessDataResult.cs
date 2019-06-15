using DotNetCore.Objects;
using Newtonsoft.Json;

namespace Framework.Objects
{
    public sealed class CustomSuccessDataResult<T> : DataResult<T>
    {
        [JsonConstructor]

        public CustomSuccessDataResult(T data, string message)
            : base(data, true, message)
        {
        }
        public CustomSuccessDataResult(T data)
            : base(data, true)
        {
        }
        public CustomSuccessDataResult(string message)
            : base(default (T), true, message)
        {
        }
        public CustomSuccessDataResult()
            : base(default (T), true)
        {
        }
    }
}