using System;

namespace DotNetCoreX.Repositories.Interfaces
{
    public interface IEntity<TPk> where TPk : IComparable 
    {
        TPk Id { get; set; }
    }
}
