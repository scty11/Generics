using System;
using System.Collections.Generic;
namespace DataStructures
{
    interface IBuffer<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }
        T Read();
        void Write(T value);
    }
}
