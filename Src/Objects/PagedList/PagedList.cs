using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreX.Objects
{
    public class PagedList<T>
    {
       
        public long Count { get; }

        public IEnumerable<T> List { get; }
    }
}
