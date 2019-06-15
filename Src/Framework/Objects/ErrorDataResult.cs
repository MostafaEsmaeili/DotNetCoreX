using DotNetCore.Objects;
using Newtonsoft.Json;

namespace Framework.Objects
{
    public sealed class CustomErrorDataResult<T> : DataResult<T>
    {
        [JsonConstructor]
        public CustomErrorDataResult(T data, string message)
            : base(data, false, message)
        {
        }
        
        public CustomErrorDataResult(T data)
            : base(data, false)
        {
        }

        public CustomErrorDataResult(string message)
            : base(default (T), false, message)
        {
        }

        public CustomErrorDataResult()
            : base(default (T), false)
        {
        }
    }
}